using System;
using System.Windows.Forms;
using BigDataAnalyzer.Options;

namespace BigDataAnalyzer.Forms
{
    public partial class OptionsForm : Form
    {
        OptionsClass optionsClass;

        public OptionsForm()
        {
            InitializeComponent();

            optionsClass = new OptionsClass();

            WriteNewOptionsContent(new string[] { "Столбец 1", "Столбец 2", "Столбец 3" });
        }

        #region FUNCTIONS

        /// <summary>
        /// Fuction write an options from loaded data
        /// </summary>
        /// <param name="columnsCount"></param>
        public void WriteNewOptionsContent(string[] columnNames)
        {
            FillFormData(columnNames); // Fill form content

            SelectCommonOptions(); // Select common
            ChangeOptionsParameters(); // Apply changes to options class
        }

        public int GetClassifierIndex()
        {
            return optionsClass.ClassifierColumn;
        }
        public int GetFirstDataIndex()
        {
            return optionsClass.DataColumn[0];
        }
        public int GetSecondDataIndex()
        {
            return optionsClass.DataColumn[1];
        }

        void SelectCommonOptions()
        {
            comboBoxClassifierColumnParam.SelectedItem = comboBoxClassifierColumnParam.Items[0];
            comboBoxFirstDataColumnParam.SelectedItem = comboBoxFirstDataColumnParam.Items[0];
            comboBoxSecondDataColumnParam.SelectedItem = comboBoxSecondDataColumnParam.Items[0];
        }

        /// <summary>
        /// Function fills all data to form content
        /// </summary>
        void FillFormData(string[] columns)
        {
            // Firstly, remove all old data
            comboBoxClassifierColumnParam.Items.Clear();
            comboBoxFirstDataColumnParam.Items.Clear();
            comboBoxSecondDataColumnParam.Items.Clear();

            object[] names = new object[columns.Length];
            for(int i = 0; i < names.Length; i++)
            {
                names[i] = columns[i];
            }

            // just add a name to comboBox, indexes in comboBox are equal like in Associative array
            comboBoxClassifierColumnParam.Items.AddRange(names);
            comboBoxFirstDataColumnParam.Items.AddRange(names);
            comboBoxSecondDataColumnParam.Items.AddRange(names);
        }

        void CancelForm()
        {
            this.Close();
        }

        /// <summary>
        /// This function set new data to options class
        /// </summary>
        void ChangeOptionsParameters()
        {
            optionsClass.ClassifierColumn = (int)comboBoxClassifierColumnParam.SelectedIndex;
            optionsClass.DataColumn[0] = (int)comboBoxFirstDataColumnParam.SelectedIndex;
            optionsClass.DataColumn[1] = (int)comboBoxSecondDataColumnParam.SelectedIndex;
        }

        #endregion

        #region UIFunctions
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            CancelForm();
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ChangeOptionsParameters();
            this.DialogResult = DialogResult.OK;
        }

        private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        #endregion
    }
}
