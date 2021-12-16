using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;

namespace Diplo
{
    public partial class ChangeInterfaceForm : Form
    {
        public IPHostEntry IPHost; // IP данного компьютера
        public Int32 RowsNum; // Количество строк
        static public String DomainName;
        
        public ChangeInterfaceForm()
        {
            InitializeComponent();

            // Самое первое, что необходимо сделать - выбрать интефрейс,
            // в котором будем искать TCP-сервера
            IPHost = DetermineHostIP();

            DetermineIPv4Addresses();

            DomainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
        }

        public string GetFullDomainName()
        {
            string domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
            string hostName = Dns.GetHostName();

            domainName = "." + domainName;
            if (!hostName.EndsWith(domainName))  // если имя хоста еще не включает имя домена
            {
                hostName += domainName;   // добавляем часть доменного имени
            }

            return hostName;
        }

        private IPHostEntry DetermineHostIP()
        {
            return Dns.GetHostEntry(Dns.GetHostName());
        }

        private void ClearAllInterfacesGrid()
        {
            InterfacesGrid.Rows.Clear();
        }

        private void DetermineIPv4Addresses()
        {
            // Был получен список интерфейсов. Нужно отделить v6 от v4,
            // так как использоваться будет именно v4

            String RightAddressFamily = "InterNetwork";
            for (int i = 0; i < IPHost.AddressList.Length; i++)
            {
                String AddressFamilyStr = IPHost.AddressList[i].AddressFamily.ToString();
                if (AddressFamilyStr == RightAddressFamily)
                {   // Добавляем адрес в таблицу
                    AddRowToInterfacesGrid(i);
                }
            }
        }

        private void AddRowToInterfacesGrid(int AddressListNum)
        {   // Добавляем строку
            object[] AddedComponent = new object[] { InterfacesGrid.Rows.Count + 1, IPHost.AddressList[AddressListNum].ToString() };

            InterfacesGrid.Rows.Add(AddedComponent);
        }

        private void SelectInterfaceButton_Click(object sender, EventArgs e)
        {            
            Int32 CurRow = InterfacesGrid.CurrentRow.Index;

            String[] InterfaseStr = new String[] { };
            for(int i = 0; i < InterfacesGrid.CurrentRow.Cells.Count; i++)
            {
                Array.Resize<String>(ref InterfaseStr, InterfaseStr.Length + 1);
                InterfaseStr[InterfaseStr.Length - 1] = InterfacesGrid.CurrentRow.Cells[i].Value.ToString();
            }

            // Интерфейс был выбран, приступаем к выбору компьютера из списка серверов
            Form SelectServerForm = new SelectServerForm(InterfaseStr[1], DomainName);
            this.Hide();
            SelectServerForm.ShowDialog();
            SelectServerForm.Activate();
            this.Close();
        }
    }
}
