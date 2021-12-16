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
    public partial class SelectServerForm : Form
    {
        IPAddress InterfaceIP;
        IPAddress SubnetMask;
        IPAddress BroadcastAddress;
        Broadcast SendBroadcast;

        // Здесь хранятся переменные для таймера прослушки серверов
        // прослушка будет вестись в отдельном потоке
        bool TimerForCheckServersIsOver;
        Thread CheckServerListThread;
        System.Timers.Timer TimerForCheck;
        int CheckingTime;

        String FileName;
        String PathToFile;
        String ThisPCDomainNameStr;
        
        public SelectServerForm(String nInterfaceIP, String nDomainName)
        {
            InitializeComponent();
            openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            ThisPCDomainNameStr = nDomainName;

            // Производим разбор строки с IP адресом интерфейса сети
            InterfaceIP = IPAddress.Parse(nInterfaceIP);

            // Находим маску сети для данного интерфейса,
            // нужен для обнаружения сервера (broadcast address)
            SubnetMask = FindIPv4MaskForThisIP(InterfaceIP);

            // Просчитываем Broadcast address по IP и маске сети
            BroadcastAddress = GetBroadcastAddress(InterfaceIP, SubnetMask);

            SendBroadcast = new Broadcast(BroadcastAddress, InterfaceIP);
            CheckServerListThread = null;
            CheckingTime = 5000;
            PathToFile = "";

            BeginCheckServerList();
        }

        void BeginCheckServerList()
        {
            if (CheckServerListThread != null)
            {
                if (CheckServerListThread.IsAlive == false)
                {
                    TimerForCheck.Interval = CheckingTime;

                    StartCheckingThread();
                }
            }                
            else
            {
                StartCheckingThread();
            }            
        }

        void StartCheckingThread()
        {
            TimerForCheckServersIsOver = false;

            CheckServerListThread = new Thread(CheckServerList);
            CheckServerListThread.Name = "CheckServerListThread";

            // Поток из основного состояния переводится в фоновое
            // это позволит основному потоку завершить работу в любом случае по звершению
            // исполнения кода, при этом данный поток выключится в любом случае
            CheckServerListThread.IsBackground = true;

            CheckServerListThread.Start();
        }

        private void ClearAllServersGrid()
        {
            ServersGrid.Rows.Clear();
        }

        public void AddRowToServersGrid(String[] ServerInfo)
        {   // Добавляем строку
            object[] AddedComponent;
            try
            {
                AddedComponent = new object[] { ServersGrid.Rows.Count + 1, ServerInfo[0], ServerInfo[1], ServerInfo[2], ServerInfo[3] };
            }
            catch(Exception e)
            {
                return;
            }

            if (ServersGrid.InvokeRequired) //Если обратились не из того потока, в котором конрол был создан, то
                //Вызываем этот же метод через Invoke
                ServersGrid.Invoke((Action<String[]>)AddRowToServersGrid, new object[] {ServerInfo});
            else
                ServersGrid.Rows.Add(AddedComponent);
        }

        void CheckServerList()
        {
            TimerForCheck = new System.Timers.Timer(CheckingTime);
            TimerForCheck.Elapsed += HandleTimer;
            TimerForCheck.AutoReset = false;
            TimerForCheck.Enabled = true;
            TimerForCheck.Start();
            while (!TimerForCheckServersIsOver) // 5 секунд ждем ответа
            {
                if(SendBroadcast.ListOfServers.Count != 0)
                {
                    lock (SendBroadcast.ListOfServers)
                    {
                        for(int i = 0; i < SendBroadcast.ListOfServers.Count; i++)
                        {   // Добавляем всю найденную на текущий момент времени информацию
                            String[] ServerInfo = SendBroadcast.ListOfServers[i];
                            AddRowToServersGrid(ServerInfo);
                        }
                        // Очищаем список после всей работы
                        SendBroadcast.ListOfServers.Clear();
                    }
                }
                Thread.Sleep(50);
            }
        }

        private void HandleTimer(Object source, ElapsedEventArgs e)
        {
            TimerForCheckServersIsOver = true; 
        }

        IPAddress FindIPv4MaskForThisIP(IPAddress address)
        {
            foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation unicastIPAddressInformation in adapter.GetIPProperties().UnicastAddresses)
                {
                    if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        if (address.Equals(unicastIPAddressInformation.Address))
                        {
                            return unicastIPAddressInformation.IPv4Mask;
                        }
                    }
                }
            }
            throw new ArgumentException(string.Format("Невозможно найти маску подсети для данного IP адресса: '{0}'", address));
        }

        IPAddress GetBroadcastAddress(IPAddress address, IPAddress subnetMask)
        {
            byte[] ipAdressBytes = address.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Длина IP адреса и маски подсети не одинакова.");

            byte[] broadcastAddress = new byte[ipAdressBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] | (subnetMaskBytes[i] ^ 255));
            }
            return new IPAddress(broadcastAddress);
        }

        private void NoServerButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PathToFile = openFileDialog1.FileName;
                FileName = openFileDialog1.SafeFileName;
                Encoding Encoding; // Хранит в себе кодировку файла

                //System.IO.Stream fs = new System.IO.FileStream(PathToFile, (System.IO.FileMode.Open));
                //using (System.IO.StreamReader sr = new System.IO.StreamReader(fs, true))
                //{
                //    String gfdg = sr.CurrentEncoding.ToString();
                //    gfdg += "gdg";
                //    Encoding = sr.CurrentEncoding;
                //}
                //fs.Close();

                //Encoding enc = Encoding.GetEncoding("windows-1251");

                // Читаем из файла в нужной кодировке
                String[] FileContains = System.IO.File.ReadAllLines(openFileDialog1.FileName, Encoding.GetEncoding("windows-1251"));
                //if(Encoding != Encoding.Unicode)
                //{   // Если кодировка была не Unicode, то нужно перевести все содержиое в Unicode
                //    for(int i = 0; i < FileContains.Length; i++)
                //    {
                //        byte[] OriginalBytesString = Encoding.GetBytes(FileContains[i]);
                //        byte[] ConvertedByteString = Encoding.Convert(Encoding,
                //        Encoding.Unicode, OriginalBytesString);
                //        FileContains[i] = Encoding.Unicode.GetString(ConvertedByteString);
                //    }
                //}

                Form TextEditor = new TextEditor(InterfaceIP, InterfaceIP, PathToFile, FileName, FileContains, ThisPCDomainNameStr);
                this.Hide();
                TextEditor.ShowDialog();
                TextEditor.Activate();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearAllServersGrid();
            SendBroadcast.SendBroadcast();
            
            BeginCheckServerList();
        }

        private void ServerSelectedButton_Click(object sender, EventArgs e)
        {
            if(ServersGrid.SelectedRows.Count != 0) // Есть выделенные сервера
            {
                IPAddress SelectedServerIP = IPAddress.Parse(ServersGrid.SelectedRows[0].Cells[1].Value.ToString());
                String SelectedServer = SelectedServerIP.ToString();
                String SelectedFilePath = ServersGrid.SelectedRows[0].Cells[4].Value.ToString();

                String Message = String.Join(" ", new object[] { "Сейчас будет произведено подключение к серверу",
                SelectedServer, ",будет произведена загрузка содержимого файла", "\nВы уверены, что хотите подключиться к данному серверу" });
                DialogResult Result = new DialogResult();
                Result = MessageBox.Show(Message, "Подтвердите выбор сервера", MessageBoxButtons.YesNo);

                if (Result == DialogResult.Yes)
                {   // Получем имя файла от сервера
                    //ServersGrid

                    Form TextEditor = new TextEditor(InterfaceIP, SelectedServerIP, "FILE_DESTINATION_ON_OTHER_SERVER", SelectedFilePath, null, ThisPCDomainNameStr);
                    this.Hide();
                    TextEditor.ShowDialog();
                    TextEditor.Activate();
                    this.Close();
                }
            }            
        }
    }
}
