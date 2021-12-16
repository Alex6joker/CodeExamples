using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPSSEmu
{
    /// <summary>
    /// Главное окно формы. Содержит информацию о пользовательском интерфейсе программы.
    /// </summary>
    public partial class MainWindowForm : Form
    {
        public MainWindowForm()
        {
            InitializeComponent();
            openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            
            // Временно
            LoadFromFile("Test1.txt");
        }

        /// <summary>
        /// Описание действий по нажатию кнопки загрузки из файла.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadFileButton_Click(object sender, EventArgs e)
        {
            LoadFromFile("Test1.txt");
        }

        /// <summary>
        /// Функция, реализующая загрузку исходного кода в TextBox программы.
        /// </summary>
        /// <param name="FilePath"></param>
        void LoadFromFile(String FilePath)
        {
            CodeTextBox.LoadFile(FilePath, RichTextBoxStreamType.PlainText);
        }

        /// <summary>
        /// Функция запуска процесса анализа исходного кода и последующей эмуляции.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartEmulationButton_Click(object sender, EventArgs e)
        {
            new Emulator.EmulatorMain(new CodeAnalyzer(CodeTextBox.Text).CompileSourceCodeLineByLine(
                new CodeAnalyzer(CodeTextBox.Text).ConvertTextBoxTextToLineByLineStringArray()));
        }
    }
}
