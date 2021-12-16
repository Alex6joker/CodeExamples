using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Timers;
using System.Collections;

namespace Diplo
{
    // Данный класс несет ответственность за
    // прием новых клиентов и начало их записи
    // в маркерное кольцо (запуск объекта класса,
    // ответственного за перестройку кольца
    public class TCPServerPart
    {
        public IPAddress MyIP;
        int ListeningPort;
        TokenRing ThisPCRingPart;
        MessageQueueManager QueueManager;

        Thread ThreadForListen;
        public Thread WorkWithReceivedMessageThread;

        List<Thread> ThreadsForWaitClientsMsgs;

        // В маркерной сети при правильной работе
        // из данного списка будут браться клиенты
        // и убираться по завершению перестройки кольца
        public List<IPAddress> ClientsList;
        public Hashtable ClientsIPs_Streams;

        public TCPServerPart(IPAddress nInterfaceIP, int nPort, ref TokenRing TR, MessageQueueManager nQueueManager)
        {
            ListeningPort = nPort;
            MyIP = nInterfaceIP;
            ThisPCRingPart = TR;
            QueueManager = nQueueManager;

            ThreadsForWaitClientsMsgs = new List<Thread>();
            ClientsList = new List<IPAddress>();
            ClientsIPs_Streams = new Hashtable();

            try
            {
                ThreadForListen = new Thread(StartListenClients);

                ThreadForListen.IsBackground = true;

                ThreadForListen.Start();
            }
            catch(ThreadStartException ex)
            {
                ThreadForListen = new Thread(StartListenClients);

                ThreadForListen.IsBackground = true;

                Thread.Sleep(1000);
                ThreadForListen.Start();
            }
        }

        void RemoveClientData(IPAddress ClientIP, NetworkStream Stream)
        {
            object locker = new object();
            lock (locker)
            {
                if (ClientsList.Contains(ClientIP))
                {
                    ClientsList.Remove(ClientIP);
                    ClientsIPs_Streams.Remove(ClientIP);
                    Stream.Close();
                }
            }            
        }

        void StartListenClients()
        {
            TcpListener Server = null;
            try
            {
                Server = new TcpListener(MyIP, ListeningPort);

                // запуск слушателя
                Server.Start();

                while (true)
                {
                    Console.WriteLine("Ожидание подключений... ");

                    // получаем входящее подключение
                    TcpClient Client = Server.AcceptTcpClient();
                    Console.WriteLine("Подключен клиент. Выполнение запроса...");

                    // получаем сетевой поток для чтения и записи
                    NetworkStream Stream = Client.GetStream();

                    try
                    {
                        Thread WaitingThread = new Thread(WaitMessagesFromClient);
                        WaitingThread.IsBackground = true;

                        ThreadsForWaitClientsMsgs.Insert(ThreadsForWaitClientsMsgs.Count, WaitingThread);

                        object[] f = new object[] { Stream, ((IPEndPoint)Client.Client.RemoteEndPoint).Address};
                        WaitingThread.Start(f);  
                    }    
                    catch(ThreadStartException ex)
                    {
                        Thread WaitingThread = new Thread(WaitMessagesFromClient);
                        WaitingThread.IsBackground = true;

                        Thread.Sleep(1000);
                        object[] f = new object[] { Stream, ((IPEndPoint)Client.Client.RemoteEndPoint).Address };
                        WaitingThread.Start(f);
                    }

                    AnalizeNewClient(Client, Stream);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (Server != null)
                    Server.Stop();
            }
        }

        void AnalizeNewClient(TcpClient Client, NetworkStream Stream)
        {
            // Узнаем IP клиента
            String EP = ((IPEndPoint)Client.Client.RemoteEndPoint).Address.ToString();

            ClientsList.Insert(ClientsList.Count, IPAddress.Parse(EP));
            try
            {
                ClientsIPs_Streams.Add(IPAddress.Parse(EP), Stream);
            }
            catch
            {
                Console.WriteLine("An element with Key = \"txt\" already exists.");
            }
        }

        void WaitMessagesFromClient(object Params)
        {
            try
            {
                while (true)
                {
                    object[] vParams = (object[])Params;
                    NetworkStream Stream = (NetworkStream)vParams[0];
                    byte[] Data = new byte[65536];

                    StringBuilder RecvStr = new StringBuilder();

                    do
                    {
                        int bytes = Stream.Read(Data, 0, Data.Length);
                        RecvStr.Append(Encoding.Unicode.GetString(Data, 0, bytes));
                    }
                    while (Stream.DataAvailable); // пока данные есть в потоке

                    String ReceivedString = RecvStr.ToString();

                    if (ReceivedString.Length == 0)
                    {   // Строка нулевой длины означает корректное отключение клиента
                        RemoveClientData((IPAddress)vParams[1], (NetworkStream)ClientsIPs_Streams[(IPAddress)vParams[1]]);
                        return;
                    }

                    MessageStringConstructorAndParser MessageParser = new MessageStringConstructorAndParser(NetworkProtocols.NETWORK_PROTOCOL_TCP, ReceivedString);
                    ReceivedString = MessageParser.ParseReceivedString();
                    // Если IP клиента соответсвует IP сервера, то значит надо запустить маркер в сеть,
                    // так как она была только что создана
                    IPAddress ClientIP = (IPAddress)vParams[1];

                    String[] SplittedString = ReceivedString.Split(new String[] { MessageStringConstructorAndParser.MessageDataSeparator }, StringSplitOptions.RemoveEmptyEntries);
                    int MessageType = Convert.ToInt32(SplittedString[0]);
                    if (MessageType == (int)TokenRingMessageTypes.MSG_RESTRUCT_TOKEN_RING)
                    {
                        int Lap = 1;
                        String Str = String.Join(MessageStringConstructorAndParser.MessageDataSeparator,
                            new object[] { (int)TokenRingMessageTypes.MSG_RESTRUCT_TOKEN_RING, Lap.ToString(), ClientIP });
                        QueueManager.AddStringMessageToQueue(Str);
                        return;
                    }
                    else if (MessageType == (int)TokenRingMessageTypes.MSG_DELETE_USER)
                    {
                        QueueManager.AddStringMessageToQueue(ReceivedString);
                        return;
                    }
                    else if (MessageType == (int)TokenRingMessageTypes.MSG_FORCED_RESTRUCT_TOKEN_RING)
                    {
                        int Lap = 1;
                        String Str = String.Join(MessageStringConstructorAndParser.MessageDataSeparator,
                            new object[] { (int)TokenRingMessageTypes.MSG_FORCED_RESTRUCT_TOKEN_RING, Lap.ToString(), ClientIP });
                        TextEditor.ForcedQueueManager.AddStringMessageToQueue(Str);
                        //return;
                        continue; // В данном случае нужно оставить нить для прослушки сообщений, так как связь ведется
                        // с закрепленным в сети клиентом
                    }

                    if (ReceivedString.Length > 0)
                    {   // Обработка полученного сообщения (должна?) находиться
                        // в отдельном потоке, так как завершение нити прослушки сообщений от сервера
                        // влечет за собой полное уничтожение обработки сообщения
                        try
                        {
                            WorkWithReceivedMessageThread = new Thread(WorkWithReceivedMessage);
                            WorkWithReceivedMessageThread.IsBackground = true;

                            WorkWithReceivedMessageThread.Start(ReceivedString);
                            WorkWithReceivedMessageThread.Join();
                        }
                        catch(ThreadStartException ex)
                        {
                            WorkWithReceivedMessageThread = new Thread(WorkWithReceivedMessage);
                            WorkWithReceivedMessageThread.IsBackground = true;

                            Thread.Sleep(1000);
                            WorkWithReceivedMessageThread.Start(ReceivedString);
                            WorkWithReceivedMessageThread.Join();
                        }
                        //WorkWithReceivedMessage(ReceivedString);
                    }
                } 
            }
            catch(Exception ex)
            {
                return;
            }
        }

        void WorkWithReceivedMessage(object StrObj)
        {
            String RecvStr = (String)StrObj;

            TCPMessageWorker MsgWorker = new TCPMessageWorker(QueueManager, TextEditor.TCPClient, ThisPCRingPart);
            MsgWorker.AnalizeReceivedMessage(RecvStr);
        }

        public void SendMessageToClient(String Message, NetworkStream Stream)
        {
            try
            {
                MessageStringConstructorAndParser StringConstructor = new MessageStringConstructorAndParser(NetworkProtocols.NETWORK_PROTOCOL_TCP, Message);
                Message = StringConstructor.MakeMessage();
                byte[] Data = Encoding.Unicode.GetBytes(Message);

                Stream.Write(Data, 0, Data.Length);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
        }
    }
}
