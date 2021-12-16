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
    public class TCPMessageWorker
    {
        // Данный класс несет ответственность за
        // обработку полученных сообщений от сервера,
        // с которым компьютер связан по маркерному кольцу
        MessageQueueManager QueueOfMessages;
        TCPClientPart TCPClient;
        TokenRing TokenRing;
        object Locker;
        String[] InitiatorUserInfo;
        static System.Timers.Timer TokenTimer;

        public TCPMessageWorker(MessageQueueManager nQueueOfMessages, TCPClientPart nTCPClient, TokenRing nTokenRing)
        {
            QueueOfMessages = nQueueOfMessages;
            TCPClient = nTCPClient;
            TokenRing = nTokenRing;
            Locker = new object();
            if (TokenTimer == null)
            {   // Таймер статический и существует после обработки первого сообщения
                TokenTimer = new System.Timers.Timer(5000);
                TokenTimer.Elapsed += HandleTimer;
                TokenTimer.AutoReset = false;
                TokenTimer.Start();
            }            
        }

        private void HandleTimer(Object source, ElapsedEventArgs e)
        {   // 5 секунд маркер не приходил, это является весомым поводом для его пересоздания
            DialogResult Result = new DialogResult();
            Result = MessageBox.Show("Маркера нет уже 5 секунд\nЗаново создать маркер?\nВо время потери связи данные в файле могут отличаться", "Потеря маркера", MessageBoxButtons.YesNo);
            if(Result == DialogResult.Yes)
            {
                if(TextEditor.ForcedQueueManager.GetMessageQueueElementsCount() != 0)
                {
                    NewActAnalizer(TextEditor.ForcedQueueManager.GetMessageFromMessagesQueue(), ((int)TokenRingMessageTypes.MSG_SEND_TOKEN).ToString(), 0);
                }
                else
                {
                    TokenAnalizer(0, 0);
                }                    
            }
            TokenTimer.Start();
        }

        void ResetTokenTimer()
        {
            TokenTimer.Stop();
            TokenTimer.Start();
        }

        String TryToGetStringPhrase()
        {
            String FullReturningPhrase;
            FullReturningPhrase = QueueOfMessages.GetMessageFromMessagesQueue();
            String[] SplittedPhrase = FullReturningPhrase.Split(new String[] { MessageStringConstructorAndParser.MessageDataSeparator }, StringSplitOptions.RemoveEmptyEntries);
            if (SplittedPhrase[0] == ((int)TokenRingMessageTypes.MSG_SEND_DATA).ToString())
            {   // 0 - добавление символа, 1 - удаление
                int OperationType = Convert.ToInt32(SplittedPhrase[3]);
                for (int i = 0; QueueOfMessages.GetMessageQueueElementsCount() != 0; i++ )
                {
                    String PeekedMsg = QueueOfMessages.PeekMessageFromMessagesQueue();
                    String[] SplittedPeekedMsg = PeekedMsg.Split(
                        new String[] { MessageStringConstructorAndParser.MessageDataSeparator }, StringSplitOptions.RemoveEmptyEntries);
                    if (SplittedPeekedMsg[0] == ((int)TokenRingMessageTypes.MSG_SEND_DATA).ToString())
                    {   // Если символы следуют друг за другом
                        if (OperationType == 0)
                        {
                            if (Convert.ToInt32(SplittedPeekedMsg[2]) == Convert.ToInt32(SplittedPhrase[2]) + (i + SplittedPeekedMsg[4].Length))
                            {
                                SplittedPhrase[4] += SplittedPeekedMsg[4];
                                FullReturningPhrase = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, SplittedPhrase);
                                QueueOfMessages.GetMessageFromMessagesQueue();
                            }
                            else
                            {   // перестали символы идти последовательно    
                                return FullReturningPhrase;
                            }
                        }   // удаление нескольуих символов (зажата клавиша)
                        else
                        {                            
                            if (Convert.ToInt32(SplittedPeekedMsg[2]) == Convert.ToInt32(SplittedPhrase[2]) ||
                                Convert.ToInt32(SplittedPeekedMsg[2]) == (Convert.ToInt32(SplittedPhrase[2]) - (i + 1))
                                // Также не вызываем совместного сообщения на Backspace и Delete одновременно
                                && SplittedPhrase[4].Contains(SplittedPeekedMsg[4]))
                            {
                                SplittedPhrase[4] += SplittedPeekedMsg[4];
                                FullReturningPhrase = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, SplittedPhrase);
                                QueueOfMessages.GetMessageFromMessagesQueue();
                            }
                            else
                            {
                                return FullReturningPhrase;
                            }
                        }
                    }
                    else
                    {   // команда - не пересылка данных  
                        return FullReturningPhrase;
                    }
                }   // Пустая очередь
                return FullReturningPhrase;
            }
            else // начальная команда - не пересылка данных  
                return FullReturningPhrase;
        }

        public void AnalizeReceivedMessage(String Message)
        {
            int StandartCopyingLenght = 4;
            ResetTokenTimer();
            String[] SplittedMessage = Message.Split(new String[] { MessageStringConstructorAndParser.MessageDataSeparator }, StringSplitOptions.RemoveEmptyEntries);
            InitiatorUserInfo = new String[SplittedMessage.Length];

            int MSG_TYPE = Convert.ToInt32(SplittedMessage[0]);
            if (SplittedMessage.Length > 1)
            {
                Array.Copy(SplittedMessage, 1, InitiatorUserInfo, 0, StandartCopyingLenght);
                String[] TemporaryStrArray = new String[SplittedMessage.Length];;
                Array.Copy(SplittedMessage, 0, TemporaryStrArray, 0, 1);
                if (SplittedMessage.Length > 5)
                    Array.Copy(SplittedMessage, StandartCopyingLenght + 1, TemporaryStrArray, 1, SplittedMessage.Length - StandartCopyingLenght - 1);
                    
                InitiatorUserInfo = InitiatorUserInfo.Where(n => !string.IsNullOrEmpty(n)).ToArray();
                TemporaryStrArray = TemporaryStrArray.Where(n => !string.IsNullOrEmpty(n)).ToArray();
                SplittedMessage = TemporaryStrArray;
            }
            Message = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, SplittedMessage);
            

            // Длина сообщения 1 - признак того, что никаких других операций
            // задействовано не было

            if(TokenRing.ThisPCAddress.ToString() == TokenRing.NextPCAddress.ToString())
                Thread.Sleep(20);
            // Если маркер свободен, то можно у нему првязать информацию из очереди
            if (SplittedMessage.Length == 1)
            {
                if(QueueOfMessages.GetMessageQueueElementsCount() == 0)
                {
                    if (MSG_TYPE == (int)TokenRingMessageTypes.MSG_CREATE_TOKEN)
                        TokenAnalizer(Convert.ToInt32(SplittedMessage[0]), 0);
                    else
                        TokenAnalizer(Convert.ToInt32(SplittedMessage[0]), Convert.ToInt64(InitiatorUserInfo[3]));
                }
                else
                {   // Есть сообщения в очереди сообщений, необходимо привязать сообщение
                    // к маркеру и далее отправлять его
                    String Phrase = TryToGetStringPhrase();
                    if (MSG_TYPE == (int)TokenRingMessageTypes.MSG_CREATE_TOKEN)
                        NewActAnalizer(Phrase, Message, 0);
                    else
                        NewActAnalizer(Phrase, Message, Convert.ToInt64(InitiatorUserInfo[3]));
                }
            }
            else
            {
                int MessageType = Convert.ToInt32(SplittedMessage[1]);
                if (MessageType == (int)TokenRingMessageTypes.MSG_RESTRUCT_TOKEN_RING || MessageType == (int)TokenRingMessageTypes.MSG_FORCED_RESTRUCT_TOKEN_RING)
                {
                    int Lap = Convert.ToInt32(SplittedMessage[2]);
                    if (Lap == 1)
                    {
                        if (!SplittedMessage.Contains<String>(TextEditor.TCPServer.MyIP.ToString()))
                        {
                            SplittedMessage = AppendInitiatorInfoToMessage(SplittedMessage, Convert.ToInt64(InitiatorUserInfo[3]));
                            Message = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, SplittedMessage);
                            Message = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, new object[] { Message, TextEditor.TCPServer.MyIP.ToString() });
                            TCPClient.SendMessageToServer(Message);
                        }
                        else
                        {
                            Lap++;
                            SplittedMessage[2] = Lap.ToString();
                            SplittedMessage = AppendInitiatorInfoToMessage(SplittedMessage, Convert.ToInt64(InitiatorUserInfo[3]));
                            Message = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, SplittedMessage);
                            TCPClient.SendMessageToServer(Message);
                        }
                    }
                    else if(Lap == 2)
                    {
                        if (SplittedMessage[SplittedMessage.Length - 1] != TextEditor.TCPServer.MyIP.ToString())
                        {
                            SplittedMessage = AppendInitiatorInfoToMessage(SplittedMessage, Convert.ToInt64(InitiatorUserInfo[3]));
                            Message = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, SplittedMessage);
                            TCPClient.SendMessageToServer(Message);
                        }
                        else
                        {
                            IPAddress _1stServer = TokenRing.NextPCAddress;
                            TokenRingRemakeManager TokenRingRemaker = new TokenRingRemakeManager(TokenRing, TCPClient);
                            TokenRingRemaker.MakeRemake1stStage();
                            TokenRingRemaker.ConnectToNewServer(IPAddress.Parse(SplittedMessage[3]));
                            Lap++;
                            SplittedMessage[2] = Lap.ToString();
                            SplittedMessage[3] = _1stServer.ToString();

                            SplittedMessage = AppendInitiatorInfoToMessage(SplittedMessage, Convert.ToInt64(InitiatorUserInfo[3]));
                            Message = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, SplittedMessage);
                            TCPClient.SendMessageToServer(Message);
                        }
                    }
                    else if(Lap == 3)
                    {
                        TokenRingRemakeManager TokenRingRemaker = new TokenRingRemakeManager(TokenRing, TCPClient);
                        TokenRingRemaker.MakeRemake1stStage();
                        if (Convert.ToInt32(SplittedMessage[1]) != (int)TokenRingMessageTypes.MSG_FORCED_RESTRUCT_TOKEN_RING)
                            TokenRingRemaker.ConnectToNewServer(IPAddress.Parse(SplittedMessage[3]));
                        else
                            TokenRingRemaker.ConnectToNewServer(IPAddress.Parse(SplittedMessage[4]));
                        //TokenRing.TextEditor.LogForm.AddInformationToLog(LogInformationTypes.LOG_INFO_CONNECTED_NEW_USER, )
                        EndWithMessageWorkAndGoNext(SplittedMessage);
                    }
                } 
                else if(MessageType == (int)TokenRingMessageTypes.MSG_DELETE_USER)
                {
                    if(SplittedMessage[2] != TokenRing.NextPCAddress.ToString())
                    {
                        SplittedMessage = AppendInitiatorInfoToMessage(SplittedMessage, Convert.ToInt64(InitiatorUserInfo[3]));
                        Message = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, SplittedMessage);
                        TCPClient.SendMessageToServer(Message);
                    }
                    else
                    {
                        TokenRingRemakeManager TokenRingRemaker = new TokenRingRemakeManager(TokenRing, TCPClient);
                        TokenRingRemaker.MakeRemake1stStage();
                        TokenRingRemaker.ConnectToNewServer(IPAddress.Parse(SplittedMessage[3]));
                        EndWithMessageWorkAndGoNext(SplittedMessage);
                    }
                }
                else if(MessageType == (int)TokenRingMessageTypes.MSG_DOWNLOAD_FILE_DATA)
                {
                    if(SplittedMessage[3] != TokenRing.ThisPCAddress.ToString())
                    {   // Если IP адреса не совпадают
                        int Offset = Convert.ToInt32(SplittedMessage[4]) + (TextEditor.MaxTransferredDataLenght * Convert.ToInt32(SplittedMessage[2]));
                        int TextLenght = TokenRing.TextEditor.GetTextLenght();
                        int Sended = 0;
                        String NewText = String.Empty;
                        if (Offset < TextLenght)
                        {
                            if ((TextLenght - Offset) > TextEditor.MaxTransferredDataLenght)
                            {
                                NewText = TokenRing.TextEditor.GetTextFromTextField(Offset, TextEditor.MaxTransferredDataLenght - Offset, out NewText);
                                Sended = TextEditor.MaxTransferredDataLenght;
                            }
                            else
                            {
                                NewText = TokenRing.TextEditor.GetTextFromTextField(Offset, TextLenght - Offset, out NewText);
                                Sended = TextLenght - Offset;
                            }
                        }                        
                        SplittedMessage[4] = (Convert.ToInt32(SplittedMessage[4]) + Sended).ToString();
                        if(Sended < TextLenght)
                            SplittedMessage[5] = "1";
                        if(Sended >= 32000)
                        {
                            int Cycle = Convert.ToInt32(SplittedMessage[2]);
                            Cycle++;
                            SplittedMessage[2] = Cycle.ToString();
                        }
                        if(NewText != String.Empty)
                        {
                            Array.Resize<String>(ref SplittedMessage, SplittedMessage.Length + 1);
                            SplittedMessage[SplittedMessage.Length - 1] += NewText;
                        }

                        SplittedMessage = AppendInitiatorInfoToMessage(SplittedMessage, Convert.ToInt64(InitiatorUserInfo[3]));
                        String NewFileMessage = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, SplittedMessage);
                        TCPClient.SendMessageToServer(NewFileMessage);
                    }
                    else
                    {
                        String[] FileData = new String[SplittedMessage.Length - 6];
                        Array.Copy(SplittedMessage, 6, FileData, 0, SplittedMessage.Length - 6);
                        int Offset = Convert.ToInt32(SplittedMessage[4]) + (TextEditor.MaxTransferredDataLenght * Convert.ToInt32(SplittedMessage[2]));
                        String UnionFiledata = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, FileData);
                        TokenRing.TextEditor.InsertTextToTextField(UnionFiledata, TokenRing.TextEditor.GetTextLenght());
                        EndWithMessageWorkAndGoNext(SplittedMessage);
                    }
                }
                else if (MessageType == (int)TokenRingMessageTypes.MSG_SEND_DATA)
                {   // Получили данные от другой машины
                    // эти данные представляют собой символ или текст,
                    // который нужно вставить в определенную позицию
                    int OperationType = Convert.ToInt32(SplittedMessage[4]);
                    if(SplittedMessage[2] != TokenRing.ThisPCAddress.ToString())
                    {   // Вставляем и отправляем дальше
                        int Offset = Convert.ToInt32(SplittedMessage[3]);
                        String MessageToInsert = SplittedMessage[5];
                        if (OperationType == 0)
                        {
                            TokenRing.TextEditor.InsertTextToTextField(MessageToInsert, Offset);
                        }                            
                        else
                        {
                            TokenRing.TextEditor.DeleteTextFromTextField(MessageToInsert, Offset);
                        }

                        if (OperationType == 0)
                        {
                            TokenRing.TextEditor.LogForm.AddInformationToLog(LogInformationTypes.LOG_INFO_ADD_SYMBOL, InitiatorUserInfo, SplittedMessage[5], SplittedMessage[3]);
                        }
                        else
                        {
                            TokenRing.TextEditor.LogForm.AddInformationToLog(LogInformationTypes.LOG_INFO_DELETE_SYMBOL, InitiatorUserInfo, SplittedMessage[5], SplittedMessage[3]);
                        }  
                        SplittedMessage = AppendInitiatorInfoToMessage(SplittedMessage, Convert.ToInt64(InitiatorUserInfo[3]));
                        Message = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, SplittedMessage);
                                              
                        TCPClient.SendMessageToServer(Message);
                    }
                    else
                    {
                        if (OperationType == 0)
                        {
                            TokenRing.TextEditor.LogForm.AddInformationToLog(LogInformationTypes.LOG_INFO_ADD_SYMBOL, InitiatorUserInfo, SplittedMessage[5], SplittedMessage[3]);
                        }
                        else
                        {
                            TokenRing.TextEditor.LogForm.AddInformationToLog(LogInformationTypes.LOG_INFO_DELETE_SYMBOL, InitiatorUserInfo, SplittedMessage[5], SplittedMessage[3]);
                        }
                        TokenRing.TextEditor.ResetSyncronizationStruct();
                        EndWithMessageWorkAndGoNext(SplittedMessage);
                    }
                }
                else if(MessageType == (int)TokenRingMessageTypes.MSG_GET_ALL_CLIENTS_INFO)
                {
                    if(SplittedMessage[2] != TokenRing.ThisPCAddress.ToString())
                    {
                        // Добавляем информацию о себе и отправялем по кольцу
                        Array.Resize<String>(ref SplittedMessage, SplittedMessage.Length + 3);
                        SplittedMessage[SplittedMessage.Length - 3] = TokenRing.ThisPCAddress.ToString();
                        try
                        {
                            if (TextEditor.ThisPCDomainNameStr != String.Empty)
                                SplittedMessage[SplittedMessage.Length - 2] = TextEditor.ThisPCDomainNameStr;
                            else
                                SplittedMessage[SplittedMessage.Length - 2] = "Домен отсутствует";
                        }
                        catch (Exception e)
                        {
                            SplittedMessage[SplittedMessage.Length - 2] = e.Message;
                        }
                        try
                        {
                            SplittedMessage[SplittedMessage.Length - 1] = Dns.GetHostEntry(Dns.GetHostName()).HostName;
                        }
                        catch (Exception e)
                        {
                            SplittedMessage[SplittedMessage.Length - 1] = e.Message;
                        }
                        SplittedMessage = AppendInitiatorInfoToMessage(SplittedMessage, Convert.ToInt64(InitiatorUserInfo[3]));
                        Message = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, SplittedMessage);
                        TCPClient.SendMessageToServer(Message);
                    }
                    else
                    {
                        String[] UsersInfo = new String[SplittedMessage.Length];
                        Array.Copy(SplittedMessage, 2, UsersInfo, 0, SplittedMessage.Length - 2);
                        UsersInfo = UsersInfo.Where(n => !string.IsNullOrEmpty(n)).ToArray();

                        TokenRing.TextEditor.UsersListForm.ClearUsersList();
                        TokenRing.TextEditor.UsersListForm.AddUsersToGrid(UsersInfo);
                        EndWithMessageWorkAndGoNext(SplittedMessage);
                    }
                }
            }
        }

        String ClearTokenFromMessage(String[] Message)
        {
            String ClearTokenMessage = String.Join(MessageStringConstructorAndParser.MessageDataSeparator,
                new object[] { Message[0], InitiatorUserInfo[0], InitiatorUserInfo[1], InitiatorUserInfo[2], InitiatorUserInfo[3] });
            return ClearTokenMessage;
        }

        void EndWithMessageWorkAndGoNext(String[] SplittedMessage)
        {
            String Message = ClearTokenFromMessage(SplittedMessage);
            TCPClient.SendMessageToServer(Message);
        }

        String[] AppendInitiatorInfoToMessage(String[] SplittedMessage, Int64 TimeAct)
        {
            int OldLenght = SplittedMessage.Length;
            Array.Resize<String>(ref SplittedMessage, SplittedMessage.Length + 4);
            Array.Copy(SplittedMessage, 1, SplittedMessage, 5, OldLenght - 1);
            SplittedMessage[1] = InitiatorUserInfo[0];
            SplittedMessage[2] = InitiatorUserInfo[1];
            SplittedMessage[3] = InitiatorUserInfo[2];
            SplittedMessage[4] = InitiatorUserInfo[3];
            return SplittedMessage;
        }

        void TokenAnalizer(int MessageType, Int64 TimeAct)
        {   // Данные 2 сообщения формируются только тогда, когда
            // нужно создать маркер и послать его по сети
            if (MessageType == (int)TokenRingMessageTypes.MSG_SEND_TOKEN)
            {
                String MessageSEND_TOKEN = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, new object[] { ((int)TokenRingMessageTypes.MSG_SEND_TOKEN).ToString(),
                TokenRing.ThisPCAddress.ToString(), TextEditor.ThisPCDomainNameStr == String.Empty ? "Домен отсутствует" : TextEditor.ThisPCDomainNameStr,
                TextEditor.UserName, TimeAct.ToString()});
                TCPClient.SendMessageToServer(MessageSEND_TOKEN);
                TokenRing.TextEditor.ClientsInfoCollector.StartTimer();
            }
            else if (MessageType == (int)TokenRingMessageTypes.MSG_CREATE_TOKEN)
            {
                String MessageCREATE_TOKEN = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, new object[] { ((int)TokenRingMessageTypes.MSG_SEND_TOKEN).ToString(),
                TokenRing.ThisPCAddress.ToString(), TextEditor.ThisPCDomainNameStr == String.Empty ? "Домен отсутствует" : TextEditor.ThisPCDomainNameStr,
                TextEditor.UserName, "0"});
                TCPClient.SendMessageToServer(MessageCREATE_TOKEN);
            }
        }

        void NewActAnalizer(String NewAct, String TokenInformationMessage, Int64 TimeAct)
        {
            TimeAct++;
            String Message = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, new object[] { TokenInformationMessage,
                TokenRing.ThisPCAddress, TextEditor.ThisPCDomainNameStr == String.Empty ? "Домен отсутствует" : TextEditor.ThisPCDomainNameStr,
                TextEditor.UserName, TimeAct.ToString(), NewAct });
            String[] SplittedMessage = Message.Split(new String[] { MessageStringConstructorAndParser.MessageDataSeparator }, StringSplitOptions.RemoveEmptyEntries);
            int MessageType = Convert.ToInt32(SplittedMessage[5]);
            if (MessageType == (int)TokenRingMessageTypes.MSG_RESTRUCT_TOKEN_RING)
            {   // Запрос на перестройку маркерного кольца
                // он приходит компьютеру, который будет стоять в кольце слева
                // от нового участника
                Message = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, new object[] { Message, TextEditor.TCPServer.MyIP });
                TCPClient.SendMessageToServer(Message);
            }
            else if (MessageType == (int)TokenRingMessageTypes.MSG_FORCED_RESTRUCT_TOKEN_RING)
            {   // Запрос на перестройку маркерного кольца при неожиданном обрыве соединения
                Message = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, new object[] { Message, TextEditor.TCPServer.MyIP });
                TCPClient.SendMessageToServer(Message);
            }
            else if (MessageType == (int)TokenRingMessageTypes.MSG_DOWNLOAD_FILE_DATA)
            {   // Запрос на получение данных файла
                TCPClient.SendMessageToServer(Message);
            }
            else if(MessageType == (int)TokenRingMessageTypes.MSG_DELETE_USER)
            {   // Запрос на удаление себя из кольца
                TCPClient.SendMessageToServer(Message);
                TokenRingRemakeManager RemakeManager = new TokenRingRemakeManager(TokenRing, TCPClient);
                TCPClient.DisconnectFromOldServer();
                RemakeManager.ConnectToNewServer(TokenRing.ThisPCAddress, TokenRingMessageTypes.MSG_CREATE_TOKEN);
            }
            else if(MessageType == (int)TokenRingMessageTypes.MSG_SEND_DATA)
            {   // Отправляем сообщение дальше по сети
                TCPClient.SendMessageToServer(Message);
            }
            else if(MessageType == (int)TokenRingMessageTypes.MSG_GET_ALL_CLIENTS_INFO)
            {   // Добавляем информацию о себе и отправялем по кольцу
                Array.Resize<String>(ref SplittedMessage, SplittedMessage.Length + 3);
                SplittedMessage[SplittedMessage.Length - 3] = TokenRing.ThisPCAddress.ToString();
                try
                {
                    if (TextEditor.ThisPCDomainNameStr != String.Empty)
                        SplittedMessage[SplittedMessage.Length - 2] = TextEditor.ThisPCDomainNameStr;
                    else
                        SplittedMessage[SplittedMessage.Length - 2] = "Домен отсутствует";
                }
                catch (Exception e)
                {
                    SplittedMessage[SplittedMessage.Length - 2] = e.Message;
                }
                try
                {
                    SplittedMessage[SplittedMessage.Length - 1] = Dns.GetHostEntry(Dns.GetHostName()).HostName;
                }
                catch (Exception e)
                {
                    SplittedMessage[SplittedMessage.Length - 1] = e.Message;
                }
                Message = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, SplittedMessage);
                TCPClient.SendMessageToServer(Message);
            }
        }
    }
}
