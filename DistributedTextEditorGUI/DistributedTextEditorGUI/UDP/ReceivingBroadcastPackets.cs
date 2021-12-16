using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Diplo
{
    // Данный класс необходим для ответа
    // на широковещательные рассылки по протоколу UDP

    // Прослушка сообщений происходит в отдельном потоке

    // Реализовано отключение прослушки, однако
    // работа программы предполагает постоянное прослушивание
    // возможных желающих присоединиться
    public class ReceivingBroadcastPackets
    {
        Socket Socket; // Сокет, на котором прослушиваем рассылку
        bool EndOfReceiving; // Логическая переменная, отвечает за конец прослушивания

        IPEndPoint IPEndP; // Обозначает, что прослушивать, и на каком порту
        EndPoint EndP; // Общее обозначение прослушиваемого трафика
        Byte[] Data; // Полученные данные

        object Locker = new object();
        IPAddress LocalIP;

        Thread RecvBroadcastMsgThread; // Объект нити, на которой прослушиваются сообщения
        
        public ReceivingBroadcastPackets(IPAddress nLocalIP)
        {
            EndOfReceiving = false;
            LocalIP = nLocalIP;

            // Широковещаельную рассылку слушаем только по UDP
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndP = new IPEndPoint(IPAddress.Any, 9050); // С любого IP на порту 9050

            Socket.Bind(IPEndP);
            EndP = (EndPoint)IPEndP;

            Console.WriteLine("Ready to receive…");
            Data = new Byte[1024]; // Принимаем сообщения точно не большей длины

            RecvBroadcastMsgThread = new Thread(TryReceiveBroadcastPackets);

            // Поток из основного состояния переводится в фоновое
            // это позволит основному потоку завершить работу в любом случае по звершению
            // исполнения кода, при этом данный поток выключится в любом случае
            RecvBroadcastMsgThread.IsBackground = true;

            RecvBroadcastMsgThread.Start();
        }

        void TryReceiveBroadcastPackets()
        {
            while(!EndOfReceiving)
            {                
                int Recv = Socket.ReceiveFrom(Data, ref EndP);
                string stringData = Encoding.Unicode.GetString(Data, 0, Recv);
                Console.WriteLine("received: {0} from: {1}", stringData, EndP.ToString());
                SendThisServerInformation(stringData);
                //Thread.Sleep(0);
            }
            Socket.Close();
        }

        void SendThisServerInformation(String ClientIPFullData)
        {
            // В случае приема сообщения
            // необходимо отправить ответ другому компьютеру
            // другому компьютеру нужен:
            // 1. Наш IP
            // 2. Имя компьютера
            // 3. Название рабочего файла
            String Msg1 = LocalIP.ToString();
            String Msg2 = String.Empty;
            try
            {
                Msg2 = Dns.GetHostEntry(Dns.GetHostName()).HostName;
            }
            catch(Exception e)
            {
                Msg2 = e.Message;
            }
            String Msg3 = TextEditor.FileName;

            String Msg = string.Join(MessageStringConstructorAndParser.MessageDataSeparator, new object[] { LocalIP.ToString(), 
                TextEditor.ThisPCDomainNameStr == String.Empty ? "Домен отсутствует" : TextEditor.ThisPCDomainNameStr,
                Msg2, Msg3 == String.Empty ? "Test" : Msg3 });
            // Готовимся к отправке сообщения компьютеру-желающему узнать список
            // Порт 10000
            // Парсим сообщение по протоколу UDP
            MessageStringConstructorAndParser MessageParser = new MessageStringConstructorAndParser(NetworkProtocols.NETWORK_PROTOCOL_UDP, ClientIPFullData);
            String ClientIP = MessageParser.ParseReceivedString();

            Socket SocketToTargetPC = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ClientIP), 10000);

            MessageStringConstructorAndParser MessageConstructor = new MessageStringConstructorAndParser(NetworkProtocols.NETWORK_PROTOCOL_UDP, Msg);
            Msg = MessageConstructor.MakeMessage();

            byte[] DataToTargetPC = Encoding.Unicode.GetBytes(Msg);
            SocketToTargetPC.SendTo(DataToTargetPC, iep);
            SocketToTargetPC.Close();
        }

        public void StopRecvMessages()
        {
            lock (Locker)
            {
                EndOfReceiving = true;
            }
        }
    }
}
