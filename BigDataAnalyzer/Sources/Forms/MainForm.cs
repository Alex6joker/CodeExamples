using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Accord;
using Accord.IO;
using Accord.Math;
using Accord.Statistics.Analysis;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.Neuro;
using Accord.Statistics.Models.Regression;

using AForge;

using ZedGraph;
using BigDataAnalyzer.UI;
using BigDataAnalyzer.Storage;
using BigDataAnalyzer.Teaching;
using BigDataAnalyzer.Analyzing;
using BigDataAnalyzer.Painting;
using BigDataAnalyzer.Options;
using Accord.Controls;
using BigDataAnalyzer.FileOpeners;

namespace BigDataAnalyzer.Forms
{
    /// <summary>
    ///   Classification using Decision Trees.
    /// </summary>
    /// 
    public partial class MainForm : Form
    {
        //int currentMethodID;
        OptionsForm optionsForm;
        DecisionTreeView decisionTreeView;
        AIMethodsComboBox MethodsCollectionComboBox;

        public MainForm()
        {
            InitializeComponent();

            dgvLearningSource.AutoGenerateColumns = true;

            openFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, "..//..//Resources");

            MethodsCollectionComboBox = new AIMethodsComboBox();
            MethodsCollectionComboBox.Visible = false;
            MethodsCollectionComboBox.SelectedIndex = 0;

            optionsForm = new OptionsForm();

            decisionTreeView = new DecisionTreeView();
            decisionTreeView.Visible = false;
        }

        #region FUNCTIONS

        /// <summary>
        /// Edit->Options menu item open an Options form|box
        /// </summary>
        private void OpenOptionsForm(OptionsForm optionsForm)
        {
            int[] oldIndexes = new int[3] { optionsForm.GetClassifierIndex(), optionsForm.GetFirstDataIndex(), optionsForm.GetSecondDataIndex() };
            DialogResult dialogResult = optionsForm.ShowDialog(this);
            if (dialogResult == DialogResult.OK && (oldIndexes[0] != optionsForm.GetClassifierIndex() ||
                oldIndexes[1] != optionsForm.GetFirstDataIndex() || oldIndexes[2] != optionsForm.GetSecondDataIndex()))
            {
                ChangeFormContents();
            }
        }

        /// <summary>
        /// Help->About menu item open an About form|box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAboutWindow()
        {
            new AboutBox().ShowDialog(this);
        }

        /// <summary>
        /// Changing a current method index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*
        private void ChangeCurrentMethod()
        {
            AssoocArray[] assoocArray = MethodsCollectionComboBox.GetAIAssoocArray();

            // Find method name
            for (int i = 0; i < assoocArray.Length; i++)
            {
                if (MethodsCollectionComboBox.SelectedItem.ToString().Equals(assoocArray[i].name))
                {
                    currentMethodID = assoocArray[i].ID;
                    break;
                }
            }
        }
        */

        /// <summary>
        /// Make form content changing
        /// </summary>
        void ChangeFormContents()
        {
            // always repaint a first method graphs content
            int currentMethodID = 0;

            StorageObject storageObject = StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 1);

            // Clear tabs content
            dgvLearningSource.DataSource = null;
            decisionTreeView.TreeSource = null;
            dgvTestingSource.DataSource = null;

            graphInput.GraphPane.CurveList.Clear();
            graphInput.GraphPane.GraphObjList.Clear();
            graphTeachingResult.GraphPane.CurveList.Clear();
            graphTeachingResult.GraphPane.GraphObjList.Clear();
            graphTesting.GraphPane.CurveList.Clear();
            graphTesting.GraphPane.GraphObjList.Clear();

            graphInput.Refresh();
            graphTeachingResult.Refresh();
            graphTesting.Refresh();

            // Change first tab content
            if (storageObject.GetStoredObject() != null)
            {
                dgvLearningSource.DataSource = storageObject.GetStoredObject();

                new AIPaintingClass(currentMethodID, 1, MethodsCollectionComboBox.GetAIAssoocArray().Length, 1, new object[] { },
                    new int[] {
                        optionsForm.GetFirstDataIndex(),
                        optionsForm.GetSecondDataIndex(),
                        optionsForm.GetClassifierIndex() }).StartPaint();
                new PaintingHeplers().RefreshGraph(graphInput,
                            (ZedGraphControl)StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 1).GetStoredGraphObject());
            }

            // Change second tab
            /*
            if (currentMethodID != 0)
            {
                decisionTreeView.Visible = false;
                groupBoxTreeView.Visible = false;
            }
            else
            {
                decisionTreeView.Visible = true;
                groupBoxTreeView.Visible = true;
                decisionTreeView.TreeSource = (DecisionTree)AIStorageList.getInstanse(MethodsCollectionComboBox.GetAIAssoocArray().Length).GetAIStorageObject(currentMethodID);
            }

            if (storageObject.teached == true)
            {
                object[] objToPaint = null;

                if (currentMethodID == 0)
                {
                    objToPaint = new object[] { decisionTreeView };
                }
                else if (currentMethodID == 1)
                {
                    objToPaint = new object[] { };
                }
                else if (currentMethodID == 2)
                {
                    objToPaint = new object[] { };
                }
                new AIPaintingClass(currentMethodID, 1, MethodsCollectionComboBox.GetAIAssoocArray().Length, 2, objToPaint,
                    new int[] { optionsForm.GetFirstDataIndex(),
                        optionsForm.GetSecondDataIndex(),
                        optionsForm.GetClassifierIndex() }).StartPaint();
                new PaintingHeplers().RefreshGraph(graphTeachingResult,
                             (ZedGraphControl)StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 2).GetStoredGraphObject());
            }
            */

            // Change third tab
            storageObject = StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 3);

            if (storageObject.GetStoredObject() != null)
            {
                dgvTestingSource.DataSource = storageObject.GetStoredObject();
                object[] objToPaint = null;

                if (currentMethodID == 0)
                {
                    objToPaint = new object[] {  };
                }
                else if (currentMethodID == 1)
                {
                    objToPaint = new object[] {  };
                }
                else if (currentMethodID == 2)
                {
                    objToPaint = new object[] {  };
                }

                storageObject = StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 3);

                if (storageObject.GetStored1DArray() != null)
                {
                    new AIPaintingClass(currentMethodID, 3, MethodsCollectionComboBox.GetAIAssoocArray().Length, 3, objToPaint,
                        new int[] { optionsForm.GetFirstDataIndex(),
                            optionsForm.GetSecondDataIndex(),
                            optionsForm.GetClassifierIndex() }).StartPaint();
                    new PaintingHeplers().RefreshGraph(graphTesting,
                            (ZedGraphControl)StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 4).GetStoredGraphObject());
                }
                else
                {
                    // Create a plot from input data
                    new AIPaintingClass(currentMethodID, 3, MethodsCollectionComboBox.GetAIAssoocArray().Length, 1,
                        new object[] { graphTesting },
                        new int[] { optionsForm.GetFirstDataIndex(),
                                            optionsForm.GetSecondDataIndex(),
                                            optionsForm.GetClassifierIndex() }).StartPaint();
                    new PaintingHeplers().RefreshGraph(graphTesting,
                             (ZedGraphControl)StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 3).GetStoredGraphObject());
                }
            }
        }

        /// <summary>
        /// Change form content for current method with condition
        /// </summary>
        /// <param name="oldMethodIndex">Old Method ID</param>
        /*
        private void TryChangeFormContents(int oldMethodIndex)
        {
            if (currentMethodID != oldMethodIndex)
            {
                ChangeFormContents();
            }
        }
        */

        /// <summary>
        ///   Creates and learns a Decision Tree to recognize the
        ///   previously loaded dataset using the current settings.
        /// </summary>
        /// 
        private void TeachAI()
        {
            for (int currentMethodID = 0; currentMethodID < MethodsCollectionComboBox.GetAIAssoocArray().Length; currentMethodID++)
            {
                MessageBox.Show("Обучение методом" + " " + "'" + MethodsCollectionComboBox.GetAIAssoocArray()[currentMethodID].name + "'");

                StorageObject storageObject = StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 1);

                if (storageObject.GetStoredObject() == null)
                {
                    MessageBox.Show("Сначала загрузите данные");
                    return;
                }

                new AITeachingClass(currentMethodID, 1, MethodsCollectionComboBox.GetAIAssoocArray().Length,
                    new int[] { optionsForm.GetFirstDataIndex(),
                            optionsForm.GetSecondDataIndex(),
                            optionsForm.GetClassifierIndex() }).StartTeachAI();

                // Paint teaching result
                object[] objToPaint = null;
                if (currentMethodID == 0)
                {
                    objToPaint = new object[] { decisionTreeView };
                }
                else if (currentMethodID == 1)
                {
                    objToPaint = new object[] { };
                }
                else if (currentMethodID == 2)
                {
                    objToPaint = new object[] { };
                }
                new AIPaintingClass(currentMethodID, 1, MethodsCollectionComboBox.GetAIAssoocArray().Length, 2, objToPaint,
                    new int[] { optionsForm.GetFirstDataIndex(),
                            optionsForm.GetSecondDataIndex(),
                            optionsForm.GetClassifierIndex() }).StartPaint();
                //new PaintingHeplers().RefreshGraph(graphTeachingResult,
                //                (ZedGraphControl)StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 2).GetStoredGraphObject());
            }
            MessageBox.Show("Обучение завершено");
        }

        /// <summary>
        ///   Tests the previously created tree into a new set of data.
        /// </summary>
        /// 
        private void TestData()
        {
            for (int currentMethodID = 0; currentMethodID < MethodsCollectionComboBox.GetAIAssoocArray().Length; currentMethodID++)
            {
                //MessageBox.Show("Анализ методом" + "'" + MethodsCollectionComboBox.GetAIAssoocArray()[currentMethodID].name + "'");

                if (AIStorageList.getInstanse(MethodsCollectionComboBox.GetAIAssoocArray().Length).GetAIStorageObject(currentMethodID) == null || dgvTestingSource.DataSource == null)
                {
                    MessageBox.Show("Сначала обучите модель.");
                    return;
                }

                new AIAnalyzingClass(currentMethodID, 3, MethodsCollectionComboBox.GetAIAssoocArray().Length,
                    new int[] { optionsForm.GetFirstDataIndex(),
                            optionsForm.GetSecondDataIndex(),
                            optionsForm.GetClassifierIndex() }).StartAnalyzingAI();

                // Paint analyzing result
                object[] objToPaint = null;
                if (currentMethodID == 0)
                {
                    objToPaint = new object[] {  };
                }
                else if (currentMethodID == 1)
                {
                    objToPaint = new object[] {  };
                }
                else if (currentMethodID == 2)
                {
                    objToPaint = new object[] {  };
                }
                new AIPaintingClass(currentMethodID, 3,
                    MethodsCollectionComboBox.GetAIAssoocArray().Length, 3, objToPaint,
                    new int[] { optionsForm.GetFirstDataIndex(),
                            optionsForm.GetSecondDataIndex(),
                            optionsForm.GetClassifierIndex() }).StartPaint();
                new PaintingHeplers().RefreshGraph(graphTesting,
                                (ZedGraphControl)StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 3).GetStoredGraphObject());
            }
            MessageBox.Show("Анализ завершен");
        }

        private void OpenFile(int storageType)
        {
            new FileOpenersMediator().ChooseRightOpener(storageType, openFileDialog, new DataGridView[] { dgvLearningSource, dgvTestingSource },
                MethodsCollectionComboBox.GetAIAssoocArray().Length, optionsForm, new object[] { graphInput, graphTesting });
        }

        void ClearModel()
        {
            for (int currentMethodID = 0; currentMethodID < MethodsCollectionComboBox.GetAIAssoocArray().Length; currentMethodID++)
            {
                AIStorageList.getInstanse(MethodsCollectionComboBox.GetAIAssoocArray().Length).SetAIStorageObject(null, currentMethodID);
            }
            ChangeFormContents();
        }

        #endregion

        #region UI_FUNCTIONS

        private void HelpAboutMenuItem_Click(object sender, EventArgs e)
        {
            ShowAboutWindow();
        }

        //private void MethodsCollectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
            //int oldMethodIndex = currentMethodID; // Save old index before changing
            //ChangeCurrentMethod();
            //TryChangeFormContents(oldMethodIndex);
        //}

        private void btnCreate_Click(object sender, EventArgs e)
        {
            TeachAI();
        }

        private void btnTestingRun_Click(object sender, EventArgs e)
        {
            TestData();
        }

        private void MenuTeachingFileOpen_Click(object sender, EventArgs e)
        {
            OpenFile(1);
        }

        private void MenuAnalyzingFileOpen_Click(object sender, EventArgs e)
        {
            bool teached = false;
            for (int currentMethodID = 0; currentMethodID < MethodsCollectionComboBox.GetAIAssoocArray().Length; currentMethodID++)
            {
                if (StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 1).teached == false)
                {
                    teached = false;
                    break;
                }
                else
                {
                    teached = true;
                }
            }
            if (!teached)
            {
                MessageBox.Show("Сначала обучите модель.");
                return;
            }
            OpenFile(3);
        }

        private void AnalyseCompareMenuItem_Click(object sender, EventArgs e)
        {
            ComparisonForm comparisonForm = new ComparisonForm();

            ConfusionMatrix[] confusionMatrices = new ConfusionMatrix[MethodsCollectionComboBox.GetAIAssoocArray().Length];

            for (int currentMethodID = 0; currentMethodID < MethodsCollectionComboBox.GetAIAssoocArray().Length; currentMethodID++)
            {
                confusionMatrices[currentMethodID] = (ConfusionMatrix)StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 4).GetStoredConfusionMatrix();
            }

            comparisonForm.SetNewFormContent(new DataGridView[] { dgvLearningSource, dgvTestingSource },
                (DecisionTree)AIStorageList.getInstanse(MethodsCollectionComboBox.GetAIAssoocArray().Length).GetAIStorageObject(0),
                confusionMatrices,
                MethodsCollectionComboBox.GetAIAssoocArray());
            comparisonForm.ShowDialog();
        }

        private void AnalyseClearModelMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Будет очищена модель для текущего метода анализа" + Environment.NewLine +
                "Продолжить?", "Внимание!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ClearModel();
                ChangeFormContents();
            }
        }

        private void EditOptionsMenuItem_Click(object sender, EventArgs e)
        {
            OpenOptionsForm(optionsForm);
        }

        #endregion
    }
}
