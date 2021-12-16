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

namespace Diplo
{
    // Данный класс несет ответственность за прослушивание
    // сообщений от связанного с ним по маркерному кольцу сервера
    // и передачу полученного сообщения объекту класса,
    // ответственному за работу над полученными сообщениями
    public class TCPClientPart
    {
        public IPAddress ServerIP;
        public IPAddress MyIP;
        int ServerPort;
        TcpClient Client;
        public NetworkStream ClientNetworkStream;
        MessageQueueManager QueueManager;
        NetworkUsersListForm UsersListForm;
        TokenRing TokenRing;

        Thread ReadMsgFromServerThread;

        public TCPClientPart(IPAddress nServerIP, int nPort, MessageQueueManager nQueueManager, IPAddress nMyIP,
            NetworkUsersListForm nUsersListForm, TokenRing nTokenRing)
        {
            ServerIP = nServerIP;
            ServerPort = nPort;
            QueueManager = nQueueManager;
            MyIP = nMyIP;
            ClientNetworkStream = null;
            UsersListForm = nUsersListForm;
            TokenRing = nTokenRing;
        }

        public void SetNewServerIP(IPAddress nServerIP)
        {
            ServerIP = nServerIP;
        }

        public void DisconnectFromOldServer()
        {
            Client.Client.Shutdown(SocketShutdown.Both);            
            ClientNetworkStream.Close();
            ClientNetworkStream.Dispose();
            Client.Client.Close();
            Client.Close();
            ReadMsgFromServerThread.Abort();
        }

        public bool ConnectToServer(TokenRingMessageTypes MessageType)
        {
            try
            {
                Client = new TcpClient();
                Client.Connect(ServerIP, ServerPort);

                NetworkStream nClientNetworkStream = Client.GetStream();
                ClientNetworkStream = nClientNetworkStream;

                StartWaitMessagesFromServer();             

                if (MessageType != TokenRingMessageTypes.MSG_NOTHING)
                {
                    String Message = String.Join(MessageStringConstructorAndParser.MessageDataSeparator,
                        new object[] { ((int)MessageType).ToString(),
                            TextEditor.TCPServer.MyIP,
                            TextEditor.ThisPCDomainNameStr == String.Empty ? "Домен отсутствует" : TextEditor.ThisPCDomainNameStr,
                            TextEditor.UserName, "MSG_UNKNOWN_TIME" });
                    SendMessageToServer(Message);
                }
                return true;                    
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                return false;
            }
        }
        
        public void SendMessageToServer(String Message)
        {
            try
            {
                MessageStringConstructorAndParser StringConstructor = new MessageStringConstructorAndParser(NetworkProtocols.NETWORK_PROTOCOL_TCP, Message);
                Message = StringConstructor.MakeMessage();
                byte[] Data = Encoding.Unicode.GetBytes(Message);

                ClientNetworkStream.Write(Data, 0, Data.Length);
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

        void WaitMessagesFromServer()
        {
            try
            {
                while (Client.Connected)
                {
                    byte[] Data = new byte[65536];

                    StringBuilder RecvStr = new StringBuilder();

                    do
                    {
                        int bytes = 0;
                        bytes = Client.GetStream().Read(Data, 0, Data.Length);

                        RecvStr.Append(Encoding.Unicode.GetString(Data, 0, bytes));
                    }
                    while (Client.GetStream().DataAvailable); // пока данные есть в потоке

                    String ReceivedString = RecvStr.ToString();

                    MessageStringConstructorAndParser MessageParser = new MessageStringConstructorAndParser(NetworkProtocols.NETWORK_PROTOCOL_TCP, ReceivedString);
                    ReceivedString = MessageParser.ParseReceivedString();

                    /*
                    if (ReceivedString.Length > 0)
                    {   // Обработка полученного сообщения (должна?) находиться
                        // в отдельном потоке, так как завершение нити прослушки сообщений от сервера
                        // влечет за собой полное уничтожение обработки сообщения
                        Thread nThread = new Thread(WorkWithReceivedMessage);
                        nThread.IsBackground = true;

                        nThread.Start(ReceivedString);
                        nThread.Join();
                        //WorkWithReceivedMessage(ReceivedString);
                    }
                     */
                }
            }
            catch (ThreadAbortException ex)
            {
                // Ничего не делаем
                return;
            }
            catch (System.IO.IOException ex)
            {
                String Msg = ex.Message;
                if (Msg.Contains("WSACancelBlockingCall"))
                {   // Только в ситуации корректного отключения при помощи WSACancelBlockingCall
                    // (методы cancel)
                    return;
                }

                // Рассматриваем ситуацию некорректного отключения следующей стоящей рабочей станции
                String[] ThisTokenRingClients = UsersListForm.GetThisTokenRingUsersIPs();
                // Таким образом, в списке будут все пользователи, однако мы знаем, кто именно отключился и кто мы

                if(ThisTokenRingClients.Length != 0)
                {   // Проверяем не пустой список
                    for(int i = 0; i < ThisTokenRingClients.Length; i++)
                    {
                        if(ThisTokenRingClients[i] != MyIP.ToString() && ThisTokenRingClients[i] != TokenRing.NextPCAddress.ToString())
                        {
                            // Пробуем подключиться к данному компьютеру
                            TokenRingRemakeManager Remaker = new TokenRingRemakeManager(TokenRing, this);
                            //Remaker.MakeRemake1stStage();
                            bool ConnectedToNewServer = Remaker.ConnectToNewServer(IPAddress.Parse(ThisTokenRingClients[i]), TokenRingMessageTypes.MSG_FORCED_RESTRUCT_TOKEN_RING);
                            if (ConnectedToNewServer)
                                return;
                        }
                    }                    
                }
                // Здесь окажемся только в случае того, что не было удачного подключения, или в сети были только 2 пользователя
                // - отключившийся и мы
                // Подключаемся к себе
                TokenRingRemakeManager RemakerToUs = new TokenRingRemakeManager(TokenRing, this);
                //RemakerToUs.MakeRemake1stStage();
                RemakerToUs.ConnectToNewServer(MyIP, TokenRingMessageTypes.MSG_NOTHING);
            }            
        }

        void WorkWithReceivedMessage(object StrObj)
        {
            String RecvStr = (String)StrObj;
        }

        public void StartWaitMessagesFromServer()
        {            
            try
            {
                ReadMsgFromServerThread = new Thread(WaitMessagesFromServer);
                ReadMsgFromServerThread.IsBackground = true;

                ReadMsgFromServerThread.Start();
            }
            catch(ThreadStartException ex)
            {
                ReadMsgFromServerThread = new Thread(WaitMessagesFromServer);
                ReadMsgFromServerThread.IsBackground = true;
                Thread.Sleep(1000);
                ReadMsgFromServerThread.Start();
            }
        }

        public void CloseStreamAndSocket()
        {
            // Закрываем потоки
            ClientNetworkStream.Close();
            Client.Close();
        }
    }
}
