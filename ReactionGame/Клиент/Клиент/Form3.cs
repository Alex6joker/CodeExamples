using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Клиент
{
    public partial class Form3 : Form
    {
        private Image img;
        private int TargetsCount;
        private String[] Coordinates;
        public Form3(String[] nCoordinates, int nTargetsCount) // Конструктор берет 2 начальные координаты для разного положения
        {
            InitializeComponent();
            TargetsCount = nTargetsCount;
            Coordinates = nCoordinates;
            img = Image.FromFile("Target.bmp");

            pictureBox1.Width = 50;
            pictureBox1.Height = 50;
            pictureBox1.Left = Int32.Parse(Coordinates[(TargetsCount-1)*2 + 0]);
            pictureBox1.Top = Int32.Parse(Coordinates[(TargetsCount-1)*2 + 1]);            
            pictureBox1.Image = img;
            pictureBox1.Visible = true;
            TargetsCount--;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {   // Если попали в цель, то отправляем данные на сервер
            if (TargetsCount == 0)
            {
                Byte[] ReadyByte = { 1 };
                Form1.ClientSocket.Send(ReadyByte);
                // Ожидаем, когда сервер пришлет время игры
                Byte[] Message = new Byte[5];
                Form1.ClientSocket.Receive(Message);
                String Result = System.Text.Encoding.UTF8.GetString(Message, 0, Message.Length);
                String Result2 = "Поздравляем! Вы справились за ";
                String Result3 = "с.";
                String[] Res = new String[3];
                Res[0] = Result2;
                Res[1] = Result;
                Res[2] = Result3;
                String TimeResult = String.Concat(Res);
                MessageBox.Show(TimeResult, "Вы победили!");
                this.Close();
            }
            else
            {
                pictureBox1.Left = Int32.Parse(Coordinates[(TargetsCount - 1) * 2 + 0]);
                pictureBox1.Top = Int32.Parse(Coordinates[(TargetsCount - 1) * 2 + 1]);
                pictureBox1.Image = img;
                pictureBox1.Visible = true;
                TargetsCount--;
            }
        }
    }
}
