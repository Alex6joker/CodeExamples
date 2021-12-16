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
    // Данный класс несет ответственность непосредственно
    // за операцию перестройки маркерного кольца
    public class TokenRingRemakeManager
    {
        TokenRing TokenRing;
        TCPClientPart TCPClient;

        public TokenRingRemakeManager(TokenRing nTokenRing, TCPClientPart nTCPClient)
        {
            TokenRing = nTokenRing;
            TCPClient = nTCPClient;
        }

        public void MakeRemake1stStage()
        {
            // На данный момент мы должны отключиться от старого сервера
            TCPClient.DisconnectFromOldServer();
        }

        public bool ConnectToNewServer(IPAddress NewServerIP)
        {
            IPAddress OldServerIP = TCPClient.ServerIP;
            TCPClient.SetNewServerIP(NewServerIP);
            bool ConnectionIsCorrect = TCPClient.ConnectToServer(TokenRingMessageTypes.MSG_NOTHING);
            if (ConnectionIsCorrect)
            {                
                TokenRing.NextPCAddress = NewServerIP;
            }
            else
            {
                TCPClient.SetNewServerIP(OldServerIP);
            }
            return ConnectionIsCorrect;       
        }

        public bool ConnectToNewServer(IPAddress NewServerIP, TokenRingMessageTypes Type)
        {
            IPAddress OldServerIP = TCPClient.ServerIP;
            TCPClient.SetNewServerIP(NewServerIP);
            bool ConnectionIsCorrect = TCPClient.ConnectToServer(Type);
            if (ConnectionIsCorrect)
            {
                TokenRing.NextPCAddress = NewServerIP;
            }
            else
            {
                TCPClient.SetNewServerIP(OldServerIP);
            }
            return ConnectionIsCorrect;
        }
    }
}
