using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplo
{
    public partial class NetworkUsersListForm : Form
    {
        public int LenghtOfColumns;
        String[] ThisTokenRingClientsIPs;
        
        public NetworkUsersListForm()
        {
            InitializeComponent();

            LenghtOfColumns = UsersGrid.ColumnCount - 1;

            ClearUsersList();
        }

        public void ClearUsersList()
        {
            if (UsersGrid.InvokeRequired)
                UsersGrid.Invoke((Action)ClearUsersList, new object[] { });                
            else
                UsersGrid.Rows.Clear();
        }

        public void AddUsersToGrid(String[] UserInfo)
        {
            if (UsersGrid.InvokeRequired) //Если обратились не из того потока, в котором конрол был создан, то
                //Вызываем этот же метод через Invoke
                UsersGrid.Invoke((Action<String[]>)AddUsersToGrid, new object[] { UserInfo });
            else
            {
                int Lenght = UserInfo.Length / LenghtOfColumns;
                for(int i = 0; i < Lenght; i++)
                {
                    // Добавляем строку
                    object[] AddedComponent = new object[] { UsersGrid.Rows.Count + 1, UserInfo[0 + LenghtOfColumns * i],
                        UserInfo[1 + LenghtOfColumns * i], UserInfo[2 + LenghtOfColumns * i] };

                    UsersGrid.Rows.Add(AddedComponent);
                }                
            }                
        }

        private void NetworkUsersListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            Hide();
        }

        public String[] GetThisTokenRingUsersIPs()
        {
            UsersGrid.Invoke((Action)GetGetThisTokenRingUsersIPs);
            return ThisTokenRingClientsIPs;
        }

        void GetGetThisTokenRingUsersIPs()
        {
            lock (UsersGrid)
            {
                ThisTokenRingClientsIPs = new String[UsersGrid.RowCount];
                for (int i = 0; i < UsersGrid.RowCount; i++)
                {
                    ThisTokenRingClientsIPs[i] = UsersGrid.Rows[i].Cells[1].Value.ToString();
                }
            }            
        }
    }
}