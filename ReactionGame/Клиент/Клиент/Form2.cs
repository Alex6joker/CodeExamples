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
using System.Net.Sockets;

namespace Клиент
{
    public partial class Form2 : Form
    {
        private int DEFAULT_BUFFER = 320;
        public Form2()
        {
            InitializeComponent();
        }        

        public void button1_Click(object sender, EventArgs e)
        {
            Byte[] ReadyByte = {1, (Byte)Int32.Parse(textBox1.Text)};
            Form1.ClientSocket.Send(ReadyByte);
            // Ожидаем, когда сервер пришлет координаты
            Byte[] Message = new Byte[DEFAULT_BUFFER];
            Form1.ClientSocket.Receive(Message);
            String Str = System.Text.Encoding.UTF8.GetString(Message, 0, Message.Length);
            String[] Res = Str.Split(' '); // Бьем на лексемы через пробел

            Form Forma = new Form3(Res, Int32.Parse(textBox1.Text));
            this.Hide();
            Forma.ShowDialog();
            Forma.Activate();
            this.Visible = true;
        }
    }
}
