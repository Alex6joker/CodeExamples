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
    // Данный класс представляет из себя
    // общее описание устройства маркерного кольца
    public class TokenRing
    {
        public IPAddress ThisPCAddress;
        public IPAddress NextPCAddress;
        public TextEditor TextEditor; // Для связи с текстовым полем

        public TokenRing(IPAddress nThisPCAddress, IPAddress nNextPCAddress, TextEditor nTextEditor)//, TCPServerPart nTCPServer, TCPClientPart nTCPClient)
        {
            ThisPCAddress = nThisPCAddress;
            NextPCAddress = nNextPCAddress;
            TextEditor = nTextEditor;
        }

        void SetNewNextPCAddress(IPAddress nNextPCIP)
        {
            lock (NextPCAddress)
            {
                NextPCAddress = nNextPCIP;
            }           
        }
    }    

    public enum TokenRingMessageTypes
    {
        MSG_SEND_TOKEN = 0,
        MSG_SEND_DATA,
        MSG_DOWNLOAD_FILE_DATA,
        MSG_RESTRUCT_TOKEN_RING,
        MSG_CREATE_TOKEN,
        MSG_CONNECT_NEW_USER,
        MSG_DELETE_USER,
        MSG_GET_ALL_CLIENTS_INFO,
        MSG_FORCED_RESTRUCT_TOKEN_RING,
        MSG_NOTHING
    }
}
