using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplo
{
    // Данный класс ответственнен за построение
    // сообщений, передающихся по сети
    public class MessageStringConstructorAndParser
    {
        NetworkProtocols CurrentProtocol;
        String MessageData;
        public String[] MessageHeaders;
        public String[] MessageEnds;
        public String DataBlockHeader;
        String ReturningString;
        public static String MessageDataSeparator;

        // В разных случаях nMessageData - сообщение для отправки или парсинга
        public MessageStringConstructorAndParser(NetworkProtocols nNetworkProtocol, Object nMessageData)
        {
            CurrentProtocol = nNetworkProtocol;
            MessageData = (String)nMessageData;
            MessageHeaders = new String[] { "MSG_UDP_UNIQUEMESSAGE", "MSG_TCP_UNIQUEMESSAGE" };
            MessageEnds = new String[] { "MSG_UDP_ENDOFMESSAGE", "MSG_TCP_ENDOFMESSAGE" };
            ReturningString = System.String.Empty;
            DataBlockHeader = "DATA_BLOCK_START";
            MessageDataSeparator = "MSG_UNIQUE_DATA_PART";
        }

        public String MakeMessage()
        {
            MakeMessageHeader();
            AssignDataLenghtToMessage();
            AssignDataBlockStartToMessage();
            AssignDataToMessage();
            AssignEOMToMessage();
            return ReturningString;
        }

        public String ParseReceivedString()
        {
            try
            {
                int[] MessageLenght_StartDataPosition = new int[2];

                TryFindMessageHeader();

                MessageLenght_StartDataPosition[0] = ReadMessageLenght();
                MessageLenght_StartDataPosition[1] = GetStartDataPosition();

                MessageData = ReadData(MessageLenght_StartDataPosition[1]);
                MessageData = CutEOMInformation(MessageData);
                return MessageData;
            }
            catch(Exception e)
            {
                return String.Empty;
            }
        }

#region MSG_CONSTRUCTION

        void MakeMessageHeader()
        {
            switch (CurrentProtocol)
            {
                case NetworkProtocols.NETWORK_PROTOCOL_UDP:
                    {
                        ReturningString = MessageHeaders[(int)CurrentProtocol];
                        break;
                    }
                case NetworkProtocols.NETWORK_PROTOCOL_TCP:
                    {
                        ReturningString = MessageHeaders[(int)CurrentProtocol];
                        break;
                    }
            }
        }

        void AssignDataLenghtToMessage()
        {
            ReturningString = String.Join(null, new object[] { ReturningString, MessageData.Length });
        }

        void AssignDataBlockStartToMessage()
        {
            ReturningString = String.Join(null, new object[] { ReturningString, DataBlockHeader });
        }

        void AssignDataToMessage()
        {
            ReturningString = String.Join(null, new object[] { ReturningString, MessageData });
        }

        void AssignEOMToMessage()
        {
            switch (CurrentProtocol)
            {
                case NetworkProtocols.NETWORK_PROTOCOL_UDP:
                    {
                        ReturningString = String.Join(null, new object[] { ReturningString, MessageEnds[(int)CurrentProtocol] });
                        break;
                    }
                case NetworkProtocols.NETWORK_PROTOCOL_TCP:
                    {
                        ReturningString = String.Join(null, new object[] { ReturningString, MessageEnds[(int)CurrentProtocol] });
                        break;
                    }
            }
        }
#endregion

#region MSG_PARSING
        bool TryFindMessageHeader()
        {   // Содержит ли данная строка нужный заголовок
            return MessageData.Contains(MessageHeaders[(int)CurrentProtocol]);
        }

        bool TryFindMessageHeader(NetworkProtocols Protocol)
        {   // Содержит ли данная строка заголовок заданного протокола
            return MessageData.Contains(MessageHeaders[(int)Protocol]);
        }

        int ReadMessageLenght()
        {   // Читаем длину сообщения
            String LenghtString = MessageData.Substring(MessageHeaders[(int)CurrentProtocol].Length,
                MessageData.IndexOf(DataBlockHeader, MessageHeaders[(int)CurrentProtocol].Length) - MessageHeaders[(int)CurrentProtocol].Length);
            return Convert.ToInt32(LenghtString);
        }

        int GetStartDataPosition()
        {
            return MessageData.IndexOf(DataBlockHeader, MessageHeaders[(int)CurrentProtocol].Length) + DataBlockHeader.Length;
        }

        String ReadData(int StartDataPosition)
        {
            return MessageData.Substring(StartDataPosition);
        }

        String CutEOMInformation(String DataWithEOM)
        {
            int DataWGarbageLenght = DataWithEOM.Length;
            int EOMIndex = DataWithEOM.IndexOf(MessageEnds[(int)CurrentProtocol]);
            return DataWithEOM.Substring(0, EOMIndex);
        }
#endregion
    }

    public enum NetworkProtocols
    {
        NETWORK_PROTOCOL_UDP = 0,
        NETWORK_PROTOCOL_TCP = 1,
    }
}
