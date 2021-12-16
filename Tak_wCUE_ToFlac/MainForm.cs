using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Flac_with_CUE_to_Tak
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }            

        private void startConvertButton_Click(object sender, System.EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Поиск будет произведен также во всех подпапках. Вы уверены, что хотите начать в этом каталоге?\n" + pathTextBox.Text, "Начать поиск", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Thread th = new Thread(() => { new ConvertationProcessor(pathTextBox.Text).TryStartAnalyzingProcess(); });
                th.IsBackground = true;
                th.Start();
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }

        #region GUI
        private void pathTextBox_TextChanged(object sender, System.EventArgs e)
        {
            if(pathTextBox.Text != null && Directory.Exists(pathTextBox.Text))
            {
                startConvertButton.Enabled = true;
            }
            else if(!Directory.Exists(pathTextBox.Text))
            {
                startConvertButton.Enabled = false;
            }
        }

        private void pathTextBox_Click(object sender, System.EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                pathTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }
        #endregion
    }
}
