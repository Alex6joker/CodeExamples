using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace Diplo
{
    // Данный класс предназначен для
    // выполнения широковещательной рассылки с целью узнать,
    // на каких компьютерах запущены сервера для кольца
    public class Broadcast
    {
        public List<String[]> ListOfServers; // Данный список представляет из себя список серверов

        IPAddress BroadcastingIP;
        IPAddress LocalIP;

        Thread RecvBroadcastMsgThread; // Объект нити, на которой прослушиваются сообщения
        
        public Broadcast(IPAddress nBroadcastingIP, IPAddress nLocalIP)
        {
            BroadcastingIP = nBroadcastingIP;
            LocalIP = nLocalIP;
            ListOfServers = new List<String[]>();
            SendBroadcast();
        }

        public void SendBroadcast()
        {
            Socket Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint iep = new IPEndPoint(BroadcastingIP, 9050);

            // Формируем сообщение, если сообщение дошло, то
            // другой компьютер должен знать наш IP, чтобы отправить ответ

            MessageStringConstructorAndParser StringConstructor = new MessageStringConstructorAndParser(NetworkProtocols.NETWORK_PROTOCOL_UDP, LocalIP.ToString());
            String Message = StringConstructor.MakeMessage();

            byte[] Data = Encoding.Unicode.GetBytes(Message);
            Socket.SendTo(Data, iep);
            Socket.Close();

            if (RecvBroadcastMsgThread == null)
            {
                RecvBroadcastMsgThread = new Thread(TryReceiveBroadcastPackets);

                // Поток из основного состояния переводится в фоновое
                // это позволит основному потоку завершить работу в любом случае по звершению
                // исполнения кода, при этом данный поток выключится в любом случае
                RecvBroadcastMsgThread.IsBackground = true;

                RecvBroadcastMsgThread.Start();
            }            
        }

        void TryReceiveBroadcastPackets()
        {
            // Слушаем ответ на порту 10000
            Socket RecvSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint IPEndP = new IPEndPoint(IPAddress.Any, 10000); // С любого IP на порту 10000

            try
            {   // Данная ошибка возникает,
                // когда уже есть сокет, слушаюший данный порт,
                // но он не успел закрыться
                RecvSocket.Bind(IPEndP);
            }
            catch (Exception e)
            {   // В таком случае ничего не предпринимаем,
                // оставляем слушать сообщения уже существующий поток и сокет
                return;
            }

            EndPoint EndP = (EndPoint)IPEndP;

            Console.WriteLine("Ready to receive…");
            while(true)
            {
                byte[] Data = new Byte[1024]; // Принимаем сообщения точно не большей длины

                int Recv = RecvSocket.ReceiveFrom(Data, ref EndP);
                String ReceivedMesasge = Encoding.Unicode.GetString(Data, 0, Recv);
                //RecvSocket.Close();

                MessageStringConstructorAndParser StringParser = new MessageStringConstructorAndParser(NetworkProtocols.NETWORK_PROTOCOL_UDP, ReceivedMesasge);
                String StringData = StringParser.ParseReceivedString();

                Console.WriteLine("received: {0} from: {1}", StringData, EndP.ToString());

                String[] SplittedString = StringData.Split(new String[] {MessageStringConstructorAndParser.MessageDataSeparator}, StringSplitOptions.RemoveEmptyEntries);
                AddServerToServerList(SplittedString);
            }            
        }

        void AddServerToServerList(String[] ServerInfo)
        {
            lock(ListOfServers)
            {
                ListOfServers.Add(ServerInfo);
            }
        }
    }
}
