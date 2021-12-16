using System;
using System.IO;
using System.Windows.Forms;

namespace BigDataAnalyzer.UI
{
    public class AIMethodsComboBox : ComboBox
    {
        AssoocArray[] AIMethodsList;

        public AIMethodsComboBox()
        {
            LoadAIMethodsList();
            FillComboBoxByMethods();
        }

        private void LoadAIMethodsList()
        {   
            // Load all info from file
            String[] methodsStringArray = File.ReadAllLines("..//..//MethodsList.txt");

            AIMethodsList = new AssoocArray[methodsStringArray.Length];

            for (int currentMethodIndex = 0; currentMethodIndex < methodsStringArray.Length; currentMethodIndex++)
            {
                // Split all separate strings to string (method name) and numeric (id)
                String[] splittedMethodsString = methodsStringArray[currentMethodIndex].Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);

                int methodID;

                try
                {
                    methodID = Convert.ToInt32(splittedMethodsString[1]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid file data format" + Environment.NewLine + ex.Message);

                    AIMethodsList = null;

                    Application.Exit();
                    return;
                }

                AIMethodsList[currentMethodIndex].name = splittedMethodsString[0];
                AIMethodsList[currentMethodIndex].ID = methodID;
            }
        }

        private void FillComboBoxByMethods()
        {   
            // Fill ComboBox
            for (int currentMethodIndex = 0; currentMethodIndex < AIMethodsList.Length; currentMethodIndex++)
            {
                this.Items.Add(AIMethodsList[currentMethodIndex].name);
            }
        }

        public AssoocArray[] GetAIAssoocArray()
        {
            return AIMethodsList;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
    }
}
