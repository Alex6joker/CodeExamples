using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;

namespace Клиент
{
    public partial class Form1 : Form
    {
        public static Socket ClientSocket;
        public Form1()
        {
            InitializeComponent();
        }        

        private void button1_Click(object sender, EventArgs e)
        {
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ClientSocket.Connect(textBox1.Text, 5050);
            Form Forma = new Form2();
            this.Hide();
            Forma.ShowDialog();
            Forma.Activate();
            this.Close();
        }
    }
}
