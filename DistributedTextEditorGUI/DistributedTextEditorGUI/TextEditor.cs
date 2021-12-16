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
    public partial class TextEditor : Form
    {
        public IPAddress InterfaceIP;
        public SymbolSynchronizationStruct SyncronizationStruct;

        TokenRing TokenRing; // Хранит в себе структуру маркерного кольца на текущий момент времени
        MessageQueueManager QueueManager; // Менеджер работы с очередью сообщений
        public static TCPServerPart TCPServer; // Требуется для прослушивания входящих клиентов в сеть
        public static TCPClientPart TCPClient; // Требуется для отправки сообщений серверу
        public static int MaxTransferredDataLenght;
        public static MessageQueueManager ForcedQueueManager; // Менеджер для работы с очередью сообщений (для разрывов соединения)

        delegate void GetTextFromTextFieldDelegate(int Offset, int Count, out String Res);
        delegate void GetTextLenghtDelegate(out int Lenght);
        GetTextLenghtDelegate GetTextLenghtDelegateVar;
        GetTextFromTextFieldDelegate GetTextFromTextFieldDelegateVar;

        public NetworkLogForm LogForm;
        public NetworkUsersListForm UsersListForm;

        public static String FilePath;
        public static String FileName;
        public static String ThisPCDomainNameStr;
        public static String UserName;
        public TokenRingClientsInfoCollector ClientsInfoCollector;

        int TCPWorkPort;

        object Locker;

        public TextEditor(IPAddress nInterfaceIP, IPAddress NextPC_IP, String nFilePath, String nFileName, String[] FileContains, String nDomainName)
        {
            InitializeComponent();
            SyncronizationStruct = new SymbolSynchronizationStruct();
            SyncronizationStruct.WordBeforeInsertAndRemove = 0;
            SyncronizationStruct.InsertedPosition = Int64.MinValue;

            LogForm = new NetworkLogForm();
            UsersListForm = new NetworkUsersListForm();
             
            ThisPCDomainNameStr = nDomainName;

            GetTextLenghtDelegateVar = new GetTextLenghtDelegate(GetTextLenghtFunc);
            GetTextFromTextFieldDelegateVar = new GetTextFromTextFieldDelegate(GetTextFromTextFieldDelegateFunction);

            MainTextEditorField.Lines = FileContains;
            saveFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            FilePath = nFilePath;
            FileName = nFileName;
            try
            {
                UserName = Dns.GetHostEntry(Dns.GetHostName()).HostName;
            }
            catch(Exception e)
            {
                UserName = "Unknown";
            }

            Locker = new object();

            InterfaceIP = nInterfaceIP;
            TCPWorkPort = 52000;
            MaxTransferredDataLenght = 32000;
            //Textlenght = MainTextEditorField.Text.Length;

            // Начинаем прослушаивать сеть, чтобы дать понять другим компьютерам,
            // что мы начали работу
            ReceivingBroadcastPackets RecvBroadcast = new ReceivingBroadcastPackets(InterfaceIP);

            QueueManager = new MessageQueueManager();
            ForcedQueueManager = new MessageQueueManager();
            ClientsInfoCollector = new TokenRingClientsInfoCollector(QueueManager);

            lock(Locker)
            {
                TokenRing = new TokenRing(InterfaceIP, NextPC_IP, this);

                TCPServer = new TCPServerPart(InterfaceIP, TCPWorkPort, ref TokenRing, QueueManager);

                // Клиент должен формироваться с уже созданным объектом маркерного кольца
                TCPClient = new TCPClientPart(NextPC_IP, TCPWorkPort, QueueManager, InterfaceIP, UsersListForm, TokenRing);
                if (InterfaceIP == NextPC_IP)
                    TCPClient.ConnectToServer(TokenRingMessageTypes.MSG_CREATE_TOKEN);
                else
                    TCPClient.ConnectToServer(TokenRingMessageTypes.MSG_RESTRUCT_TOKEN_RING);
                
                // Дает точно понять разницу, что нужно скачать содержимое файла
                if (InterfaceIP != NextPC_IP)
                {
                    // Формируем для себя строку на скачивание файла
                    // Когда сеть перестроится, мы возьмем сообщение из очереди и станем
                    // скачивать содержимое файла
                    String DownloadCommonFile = String.Join(MessageStringConstructorAndParser.MessageDataSeparator,
                        new object[] { (int)TokenRingMessageTypes.MSG_DOWNLOAD_FILE_DATA, "0", InterfaceIP.ToString(),
                        "0", "0"});
                    QueueManager.AddStringMessageToQueue(DownloadCommonFile);
                }
            }
            String gf = MainTextEditorField.Text;
        }

        public int GetSelectionPosistion()
        {
            int Ret;
            lock (Locker)
            {
                Ret = MainTextEditorField.SelectionStart;
            }
            return Ret;
        }

        #region WorkWithEnteredSymbols

        // Переменные для состояний нажатых клавиш
        bool ControlIsPressed = false;
        bool ShiftIsPressed = false;
        bool CTRL_V_Pressed = false;
        char PressedKey = ' ';
        bool SomeKeyIsPressed = false;
        bool DeleteBackspaceIsPressed = false;
        int LastDeleteBackspaceKeyCode = 0;

        bool NoSymbolsInsertedNow()
        {
            return !(DeleteBackspaceIsPressed || SomeKeyIsPressed);
        }
        
        private void MainTextEditorField_TextChanged(object sender, EventArgs e)
        {
            String Str = new string(new Char[] {PressedKey});
            if (DeleteBackspaceIsPressed)
            {
                Str = LastDeleteBackspaceKeyCode.ToString();

                if (TokenRing.ThisPCAddress != TokenRing.NextPCAddress)
                {
                    //ChangeSyncronizationStruct((int)GetSelectionPosistion(), true);
                }
                else
                {
                    ResetSyncronizationStruct();
                }  
                  
                String MessageToQueue = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, new object[] { (int)TokenRingMessageTypes.MSG_SEND_DATA,
                    InterfaceIP, GetSelectionPosistion() - 1, "1", Str});
                ChangeSyncronizationStruct(GetSelectionPosistion(), false);
                QueueManager.AddStringMessageToQueue(MessageToQueue);
                return;
            }
            if (ShiftIsPressed)
            {   // Посылаем символ в верхнем регистре
                Str.ToUpper();
                // Посылаем символ

                if (TokenRing.ThisPCAddress != TokenRing.NextPCAddress)
                {
                    //ChangeSyncronizationStruct((int)GetSelectionPosistion(), true);
                }
                else
                {
                    ResetSyncronizationStruct();
                }  
                  
                String MessageToQueue = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, new object[] { (int)TokenRingMessageTypes.MSG_SEND_DATA,
                    InterfaceIP, GetSelectionPosistion() - 1, "0", Str});
                ChangeSyncronizationStruct(GetSelectionPosistion(), true);
                QueueManager.AddStringMessageToQueue(MessageToQueue);
                return;
            }
            if (CTRL_V_Pressed)
            {   // Берем текст из буфера обмена
                Str = Clipboard.GetText();

                if (TokenRing.ThisPCAddress != TokenRing.NextPCAddress)
                {
                    //ChangeSyncronizationStruct((int)GetSelectionPosistion(), true);
                }
                else
                {
                    ResetSyncronizationStruct();
                }  
                  
                // Посылаем текст
                String MessageToQueue = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, new object[] { (int)TokenRingMessageTypes.MSG_SEND_DATA,
                    InterfaceIP, GetSelectionPosistion() - 1, "0", Str});
                ChangeSyncronizationStruct(GetSelectionPosistion(), true);
                QueueManager.AddStringMessageToQueue(MessageToQueue);
                return;
            }
            else if (SomeKeyIsPressed)
            {   // Посылаем символ

                if (Str == " ")
                    Str = "UNIQUE_SPACE_TEXT";
                else if (Str == "\r")
                    Str = "UNIQUE_NEW_LINE_TEXT";
                else if (Str == "\n")
                    Str = "UNIQUE_NEW_LINE_TEXT";
                else if (Str == "\b")
                    return;
                
                if (TokenRing.ThisPCAddress != TokenRing.NextPCAddress)
                {
                    //ChangeSyncronizationStruct((int)GetSelectionPosistion(), true);
                }
                else
                {
                    ResetSyncronizationStruct();
                }  
                  
                String MessageToQueue = String.Join(MessageStringConstructorAndParser.MessageDataSeparator, new object[] { (int)TokenRingMessageTypes.MSG_SEND_DATA,
                    InterfaceIP, GetSelectionPosistion() - 1, "0", Str});
                ChangeSyncronizationStruct(GetSelectionPosistion(), true);
                QueueManager.AddStringMessageToQueue(MessageToQueue);                
                return;
            }            
        }

        private void MainTextEditorField_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 17)
            {
                ControlIsPressed = false;
            }
            else if (e.KeyValue == 16)
            {
                ShiftIsPressed = false;
            }
            else if (e.KeyCode == Keys.V)
            {
                if (ControlIsPressed)
                    CTRL_V_Pressed = false;
            }
            else if (e.KeyValue == 46 || e.KeyValue == 8)
            {
                LastDeleteBackspaceKeyCode = 0;
                DeleteBackspaceIsPressed = false;
            }
            else
            {
                SomeKeyIsPressed = false;
            }
        }

        private void MainTextEditorField_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char PressedKey = e.KeyChar;
            this.PressedKey = PressedKey;
            SomeKeyIsPressed = true;
            if(Convert.ToInt32(this.PressedKey) == 13)
            {
                MainTextEditorField_TextChanged(null, null);
            }
        }

        private void MainTextEditorField_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control)
            {
                ControlIsPressed = true;
            }
            if (e.Shift)
            {
                ShiftIsPressed = true;
            }
            if (e.KeyValue == 46 || e.KeyValue == 8)
            {
                LastDeleteBackspaceKeyCode = e.KeyValue;
                DeleteBackspaceIsPressed = true;
            }
            // Сочетание CTRL+V
            if (ControlIsPressed && e.KeyCode == Keys.V)
            {
                CTRL_V_Pressed = true;
            }
        }

        #endregion

        #region Dialogs

        bool SaveDialog()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MainTextEditorField.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                return true;
            }
            else
                return false;
        }

        bool OpenDialog()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MainTextEditorField.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                FilePath = openFileDialog1.FileName;
                FileName = openFileDialog1.SafeFileName;
                return true;
            }
            else
                return false;
        }

        #endregion

        #region InsertDeleteText
        
        public void InsertTextToTextField(String Text, int Offset)
        {
            if (MainTextEditorField.InvokeRequired) //Если обратились не из того потока, в котором конрол был создан, то
                //Вызываем этот же метод через Invoke
                MainTextEditorField.Invoke((Action<String, int>)InsertTextToTextField, new object[] { Text, Offset });
            else
            {                
                lock (MainTextEditorField)
                {
                    int Selection = GetSelectionPosistion();
                    String LocalMsg = Text;
                    while(LocalMsg.Contains("UNIQUE_SPACE_TEXT"))
                    {
                        String UNIQUE_SPACE_TEXT = "UNIQUE_SPACE_TEXT";
                        int UNIQUE_SPACE_TEXT_INDEX = LocalMsg.IndexOf(UNIQUE_SPACE_TEXT);
                        LocalMsg = LocalMsg.Remove(UNIQUE_SPACE_TEXT_INDEX, UNIQUE_SPACE_TEXT.Length);
                        LocalMsg = LocalMsg.Insert(UNIQUE_SPACE_TEXT_INDEX, " ");
                    }
                    while (LocalMsg.Contains("UNIQUE_NEW_LINE_TEXT"))
                    {
                        String UNIQUE_NEW_LINE_TEXT = "UNIQUE_NEW_LINE_TEXT";
                        int UNIQUE_NEW_LINE_TEXT_INDEX = LocalMsg.IndexOf(UNIQUE_NEW_LINE_TEXT);
                        LocalMsg = LocalMsg.Remove(UNIQUE_NEW_LINE_TEXT_INDEX, UNIQUE_NEW_LINE_TEXT.Length);
                        LocalMsg = LocalMsg.Insert(UNIQUE_NEW_LINE_TEXT_INDEX, Environment.NewLine[1].ToString());
                    }
                    if (TokenRing.ThisPCAddress == TokenRing.NextPCAddress)
                        ResetSyncronizationStruct();
                    int TextLenght = LocalMsg.Length;
                    try
                    {
                        if (LocalMsg.Length < 200)
                            for (int i = 0; i < LocalMsg.Length; i++)
                            {
                                MainTextEditorField.Text = MainTextEditorField.Text.Insert(Offset + i, LocalMsg[i].ToString());
                            }     
                        else
                            MainTextEditorField.Text = MainTextEditorField.Text.Insert(Offset, LocalMsg);
                    }
                    catch(Exception ex)
                    {
                        //new Thread(ShowErrorMessageBox).Start("Ошибка при Insert\nВозможна рассинхронизация");
                    }
                    if (Selection >= Offset + TextLenght)
                        MainTextEditorField.SelectionStart = Selection + TextLenght;
                    else
                        MainTextEditorField.SelectionStart = Selection;
                    ResetSyncronizationStruct();
                }                
            }                           
        }

        void ShowErrorMessageBox(object Text)
        {
            MessageBox.Show((string)Text);
        }

        public void DeleteTextFromTextField(String Text, int Offset)
        {
            if (MainTextEditorField.InvokeRequired) //Если обратились не из того потока, в котором конрол был создан, то
                //Вызываем этот же метод через Invoke
                MainTextEditorField.Invoke((Action<String, int>)DeleteTextFromTextField, new object[] { Text, Offset });
            else
            {
                int Selection = GetSelectionPosistion();
                int Count = DetermineSymbolsCount(Text);
                bool IsBackSpaceOperation = Text.Contains(((int)Keys.Back).ToString());
                lock (MainTextEditorField)
                {
                    int TextLenght = Text.Length;
                    if (TokenRing.ThisPCAddress == TokenRing.NextPCAddress)
                        ResetSyncronizationStruct();
                    try
                    {                        
                        if (IsBackSpaceOperation)
                        {
                            int LocalOffset = Offset;
                            for (; Count > 0; Count--)
                            {
                                //while (Selection > Offset + TextLenght && !NoSymbolsInsertedNow())
                                //{
                                //    Thread.Sleep(0);
                                //}
                                MainTextEditorField.Text = MainTextEditorField.Text.Remove(LocalOffset + 1 + (int)SyncronizationStruct.WordBeforeInsertAndRemove, 1);
                                LocalOffset--;
                            }
                        }
                        else
                        {
                            TextLenght /= 2;
                            //while (Selection > Offset + TextLenght && !NoSymbolsInsertedNow())
                            //{
                            //    Thread.Sleep(0);
                            //}
                            MainTextEditorField.Text = MainTextEditorField.Text.Remove(Offset + 1 + (int)SyncronizationStruct.WordBeforeInsertAndRemove, Count);
                        }
                    }
                    catch(Exception ex)
                    {
                        //new Thread(ShowErrorMessageBox).Start("Ошибка при Remove\nВозможна рассинхронизация");
                    }
                    if (IsBackSpaceOperation)
                    {
                        try
                        {
                            MainTextEditorField.SelectionStart = Selection - TextLenght;
                        }
                        catch(Exception ex)
                        {
                            MainTextEditorField.SelectionStart = 0;
                        }
                    }                        
                    else
                        MainTextEditorField.SelectionStart = Selection;
                    ResetSyncronizationStruct();                 
                }
            }
        }

        int DetermineSymbolsCount(String Text)
        {
            String LocalText = Text;
            int Count = 0;
            while (LocalText.Contains(((int)Keys.Delete).ToString()))
            {
                int DELETE_INDEX = LocalText.IndexOf(((int)Keys.Delete).ToString());
                LocalText = LocalText.Remove(DELETE_INDEX, ((int)Keys.Delete).ToString().Length);
                Count++;
            }
            while (LocalText.Contains(((int)Keys.Back).ToString()))
            {
                int BACKSPACE_INDEX = LocalText.IndexOf(((int)Keys.Back).ToString());
                LocalText = LocalText.Remove(BACKSPACE_INDEX, ((int)Keys.Back).ToString().Length);
                Count++;
            }
            return Count;
        }        

        #endregion

        #region TextHelpers

        public String GetTextFromTextField(int Offset, int Count, out String Ret)
        {
            Ret = "";
            object[] args = new object[] { Offset, Count, Ret };
            MainTextEditorField.Invoke(GetTextFromTextFieldDelegateVar, args);
            return (String)args[2];
        }

        void GetTextFromTextFieldDelegateFunction(int Offset, int Count, out String Ret)
        {
            Char[] Destination = new char[MaxTransferredDataLenght];
            MainTextEditorField.Text.CopyTo(Offset, Destination, 0, Count);
            Ret = new String(Destination);
            Ret = Ret.Substring(0, Count);
        }

        public int GetTextLenght()
        {
            int Lenght = -1;
            object[] args = new object[] { Lenght };

            if (MainTextEditorField.InvokeRequired)
                MainTextEditorField.Invoke(GetTextLenghtDelegateVar, args);

            return (int)args[0]; 
        }

        public void GetTextLenghtFunc(out int Lenght)
        {
            Lenght = MainTextEditorField.Text.Length;
        }

        #endregion

        #region InterfaceClickEvents

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDialog();
        }
        private void SaveButtonPictureBox_Click(object sender, EventArgs e)
        {
            SaveDialog();
        }
        private void OpenFilePictureBox_Click(object sender, EventArgs e)
        {
            OpenFileDialogToUser();
        }
        private void отключитьсяОтСетиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisconnectionDialog();
        }
        private void DisconnectPictureBox_Click(object sender, EventArgs e)
        {
            DisconnectionDialog();
        }
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewFileDialog();
        }        
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialogToUser();
        }

        private void логСетиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogForm.Show();
        }

        private void списокПользователейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsersListForm.Show();
        }
        #endregion

        #region Disconnection

        bool DisconnectionDialog()
        {
            if (InterfaceIP.ToString() != TokenRing.NextPCAddress.ToString())
            {
                DialogResult Result = new DialogResult();
                Result = MessageBox.Show("Вы уверены, что хотите отключиться от маркерной сети " + TokenRing.NextPCAddress.ToString(),
                    "Требуется подтверждение", MessageBoxButtons.YesNo);

                if (Result == DialogResult.Yes)
                {
                    DisconnectionDialogFunc();
                    return true;
                }
                else
                    return false;
            }
            else
            {
                MessageBox.Show("Вы единственный участник сети\nОтключение невозможно", "Ошибка отключения");
                return false;
            }
        }

        void DisconnectionDialogFunc()
        {
            // В случае выхода по нажатию кнопки выхода
            // отключение от сети будет являться корректным
            if (InterfaceIP.ToString() != TokenRing.NextPCAddress.ToString())
            {   // Посылаем на сервер сообщение о том, что нужно отсоединить клиента,
                // и предоставляем для этого всю необходимую информацию
                String Message = String.Join(MessageStringConstructorAndParser.MessageDataSeparator,
                    new object[] { (int)TokenRingMessageTypes.MSG_DELETE_USER, InterfaceIP, TokenRing.NextPCAddress });
                QueueManager.AddStringMessageToQueue(Message);
                MessageBox.Show("Вы отключились от сети");
            }
        }
        #endregion

        #region CreateNew

        private void CreateNewPictureBox_Click(object sender, EventArgs e)
        {
            CreateNewFileDialog();
        }

        void CreateNewFileDialog()
        {
            if (InterfaceIP.ToString() != TokenRing.NextPCAddress.ToString())
            {
                DialogResult Result = new DialogResult();
                Result = MessageBox.Show("Для работы с новым файлом нужно отключиться от сети\nВы уверены, что хотите отключиться?",
                    "Требуется подтверждение", MessageBoxButtons.YesNo);
                if (Result == DialogResult.Yes)
                {
                    if(DisconnectionDialog())
                        CreateNewFileDialogFunc();
                }
            }
            else // Не участник сети
            {
                CreateNewFileDialogFunc();
            }
        }

        void CreateNewFileDialogFunc()
        {
            DialogResult Result = new DialogResult();
            Result = MessageBox.Show("Вы хотите сохранить данный файл?", "Требуется подтверждение", MessageBoxButtons.YesNo);
            if (Result == DialogResult.Yes)
            {
                if (SaveDialog()) // Если сохранили файл
                {
                    MainTextEditorField.Clear();
                }
            }
            else // Пользователь отказался сохранять файл
                MainTextEditorField.Clear();
        }

        #endregion

        #region OpenFile
        void OpenFileDialogToUser()
        {
            if (InterfaceIP.ToString() != TokenRing.NextPCAddress.ToString())
            {
                DialogResult Result = new DialogResult();
                Result = MessageBox.Show("Для работы с новым файлом нужно отключиться от сети\nВы уверены, что хотите отключиться?",
                    "Требуется подтверждение", MessageBoxButtons.YesNo);
                if (Result == DialogResult.Yes)
                {
                    DisconnectionDialog();
                    OpenFileDialogFunc();
                }
            }
            else // Не участник сети
            {
                OpenFileDialogFunc();
            }
        }

        void OpenFileDialogFunc()
        {
            DialogResult Result = new DialogResult();
            Result = MessageBox.Show("Вы хотите сохранить данный файл?", "Требуется подтверждение", MessageBoxButtons.YesNo);
            if (Result == DialogResult.Yes)
            {
                if (SaveDialog()) // Если сохранили файл
                {
                    OpenDialog();
                }
            }
            else // Пользователь отказался сохранять файл
                OpenDialog();
        }
        #endregion

        #region SyncronizationStruct
        public struct SymbolSynchronizationStruct
        {
            public Int64 WordBeforeInsertAndRemove;
            public Int64 InsertedPosition;
        }

        public void ChangeSyncronizationStruct(Int64 Position, bool InsertOperation)
        {
            object Locker = new object();
            lock (Locker)
            {
                if (Position == Int64.MinValue)
                {
                    SyncronizationStruct.InsertedPosition = Position;
                }
                if (InsertOperation)
                {
                    SyncronizationStruct.WordBeforeInsertAndRemove++;
                }
                else
                {
                    SyncronizationStruct.WordBeforeInsertAndRemove--;
                }
            }            
        }

        public void ResetSyncronizationStruct()
        {
            object Locker = new object();
            lock (Locker)
            {
                SyncronizationStruct.InsertedPosition = Int64.MinValue;
                SyncronizationStruct.WordBeforeInsertAndRemove = 0;
            }            
        }

        #endregion

        private void TextEditor_ResizeEnd(object sender, EventArgs e)
        {
            
        }

        private void TextEditor_Resize(object sender, EventArgs e)
        {
            DisconnectPictureBox.Location = new Point(this.Size.Width - 70, DisconnectPictureBox.Location.Y);
            MainTextEditorField.Location = new Point(0, this.Size.Height - (this.Size.Height - 99));
            MainTextEditorField.Size = new Size(this.Size.Width, this.Size.Height - MainTextEditorField.Location.Y);
        }
    }
}
