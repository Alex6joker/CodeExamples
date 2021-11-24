using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Accord.MachineLearning.DecisionTrees;
using Accord.Math;
using Accord.Statistics.Analysis;
using Accord.Statistics.Visualizations;
using BigDataAnalyzer.Painting;
using BigDataAnalyzer.Storage;
using ZedGraph;

namespace BigDataAnalyzer.Forms
{
    public partial class ComparisonForm : Form
    {
        Color[] colors = { Color.Red, Color.Blue, Color.DeepSkyBlue };

        public ComparisonForm()
        {
            InitializeComponent();

            scatterplotViewROCCurve.Graph.GraphPane.XAxis.Scale.Min = 0.0f;
            scatterplotViewROCCurve.Graph.GraphPane.XAxis.Scale.Max = 1.0f;

            scatterplotViewROCCurve.Graph.GraphPane.YAxis.Scale.Min = 0.0f;
            scatterplotViewROCCurve.Graph.GraphPane.YAxis.Scale.Max = 1.0f;
        }

        public void SetNewFormContent(DataGridView[] dataGridViewInputsAnalyze, object decisionTree, ConfusionMatrix[] confusionMatrices, AssoocArray[] methods)
        {
            #region Paint graphs
            try
            {
                new PaintingHeplers().RefreshGraph(graphInput,
                        (ZedGraphControl)StorageObjectGetter.getInstanse().GetStorageByMethodID(0, 1).GetStoredGraphObject());
            }
            catch
            {
                
            }

            try
            {
                new PaintingHeplers().RefreshGraph(graphTeachingResult1,
                        (ZedGraphControl)StorageObjectGetter.getInstanse().GetStorageByMethodID(0, 2).GetStoredGraphObject());
            }
            catch
            {

            }
            try
            {
                new PaintingHeplers().RefreshGraph(graphTeachingResult2,
                            (ZedGraphControl)StorageObjectGetter.getInstanse().GetStorageByMethodID(1, 2).GetStoredGraphObject());
            }
            catch
            {

            }
            try
            {
                new PaintingHeplers().RefreshGraph(graphTeachingResult3,
                            (ZedGraphControl)StorageObjectGetter.getInstanse().GetStorageByMethodID(2, 2).GetStoredGraphObject());
            }
            catch
            {

            }

            try
            {
                new PaintingHeplers().RefreshGraph(graphTesting1,
                            (ZedGraphControl)StorageObjectGetter.getInstanse().GetStorageByMethodID(0, 4).GetStoredGraphObject());
            }
            catch
            {

            }
            try
            {
                new PaintingHeplers().RefreshGraph(graphTesting2,
                            (ZedGraphControl)StorageObjectGetter.getInstanse().GetStorageByMethodID(1, 4).GetStoredGraphObject());
            }
            catch
            {

            }
            try
            {
                new PaintingHeplers().RefreshGraph(graphTesting3,
                            (ZedGraphControl)StorageObjectGetter.getInstanse().GetStorageByMethodID(2, 4).GetStoredGraphObject());
            }
            catch
            {

            }
            #endregion

            #region DataGrids
            try
            {
                dataGridViewInputs.DataSource = dataGridViewInputsAnalyze[0].DataSource;
                dataGridViewAnalyze.DataSource = dataGridViewInputsAnalyze[1].DataSource;
            }
            catch
            {

            }
            #endregion

            #region Decision Tree            
            try
            {
                decisionTreeView.TreeSource = (DecisionTree)decisionTree;
            }
            catch
            {

            }
            #endregion

            #region Confusion matrix to DataGrid
            try
            {
                //dgvPerformanceDecisionTree.DataSource = new[] { confusionMatrices[0] };
                dgvPerformanceDecisionTree.Rows.Add();
                dgvPerformanceDecisionTree.Rows[0].Cells[0].Value = confusionMatrices[0].TruePositives;
                dgvPerformanceDecisionTree.Rows[0].Cells[1].Value = confusionMatrices[0].FalseNegatives;
                dgvPerformanceDecisionTree.Rows[0].Cells[2].Value = confusionMatrices[0].TrueNegatives;
                dgvPerformanceDecisionTree.Rows[0].Cells[3].Value = confusionMatrices[0].FalsePositives;
                dgvPerformanceDecisionTree.Rows[0].Cells[4].Value = confusionMatrices[0].Sensitivity;
                dgvPerformanceDecisionTree.Rows[0].Cells[5].Value = confusionMatrices[0].Specificity;
                dgvPerformanceDecisionTree.Rows[0].Cells[6].Value = confusionMatrices[0].Efficiency;
                dgvPerformanceDecisionTree.Rows[0].Cells[7].Value = confusionMatrices[0].Accuracy;
            }
            catch
            {

            }
            try
            {
                dgvPerformanceLogisticRegression.Rows.Add();
                dgvPerformanceLogisticRegression.Rows[0].Cells[0].Value = confusionMatrices[1].TruePositives;
                dgvPerformanceLogisticRegression.Rows[0].Cells[1].Value = confusionMatrices[1].FalseNegatives;
                dgvPerformanceLogisticRegression.Rows[0].Cells[2].Value = confusionMatrices[1].TrueNegatives;
                dgvPerformanceLogisticRegression.Rows[0].Cells[3].Value = confusionMatrices[1].FalsePositives;
                dgvPerformanceLogisticRegression.Rows[0].Cells[4].Value = confusionMatrices[1].Sensitivity;
                dgvPerformanceLogisticRegression.Rows[0].Cells[5].Value = confusionMatrices[1].Specificity;
                dgvPerformanceLogisticRegression.Rows[0].Cells[6].Value = confusionMatrices[1].Efficiency;
                dgvPerformanceLogisticRegression.Rows[0].Cells[7].Value = confusionMatrices[1].Accuracy;
            }
            catch
            {

            }
            try
            {
                dgvPerformanceNeuralNetwork.Rows.Add();
                dgvPerformanceNeuralNetwork.Rows[0].Cells[0].Value = confusionMatrices[2].TruePositives;
                dgvPerformanceNeuralNetwork.Rows[0].Cells[1].Value = confusionMatrices[2].FalseNegatives;
                dgvPerformanceNeuralNetwork.Rows[0].Cells[2].Value = confusionMatrices[2].TrueNegatives;
                dgvPerformanceNeuralNetwork.Rows[0].Cells[3].Value = confusionMatrices[2].FalsePositives;
                dgvPerformanceNeuralNetwork.Rows[0].Cells[4].Value = confusionMatrices[2].Sensitivity;
                dgvPerformanceNeuralNetwork.Rows[0].Cells[5].Value = confusionMatrices[2].Specificity;
                dgvPerformanceNeuralNetwork.Rows[0].Cells[6].Value = confusionMatrices[2].Efficiency;
                dgvPerformanceNeuralNetwork.Rows[0].Cells[7].Value = confusionMatrices[2].Accuracy;
            }
            catch
            {

            }
            #endregion

            #region Histograms
            try
            {
                CreateHistogramPicture(methods, chart1, new double[] { confusionMatrices[0].TruePositives, confusionMatrices[1].TruePositives, confusionMatrices[2].TruePositives });
                CreateHistogramPicture(methods, chart2, new double[] { confusionMatrices[0].FalseNegatives, confusionMatrices[1].FalseNegatives, confusionMatrices[2].FalseNegatives });
                CreateHistogramPicture(methods, chart3, new double[] { confusionMatrices[0].TrueNegatives, confusionMatrices[1].TrueNegatives, confusionMatrices[2].TrueNegatives });
                CreateHistogramPicture(methods, chart4, new double[] { confusionMatrices[0].FalsePositives, confusionMatrices[1].FalsePositives, confusionMatrices[2].FalsePositives });
                CreateHistogramPicture(methods, chart5, new double[] { confusionMatrices[0].Sensitivity, confusionMatrices[1].Sensitivity, confusionMatrices[2].Sensitivity });
                CreateHistogramPicture(methods, chart6, new double[] { confusionMatrices[0].Specificity, confusionMatrices[1].Specificity, confusionMatrices[2].Specificity });
                CreateHistogramPicture(methods, chart7, new double[] { confusionMatrices[0].Efficiency, confusionMatrices[1].Efficiency, confusionMatrices[2].Efficiency });
                CreateHistogramPicture(methods, chart8, new double[] { confusionMatrices[0].Accuracy, confusionMatrices[1].Accuracy, confusionMatrices[2].Accuracy });
            }
            catch
            {

            }
            #endregion

            #region ROC Curve
            try
            {
                PaintROCCurve(methods);
            }
            catch
            {

            }
            #endregion
        }

        void PaintROCCurve(AssoocArray[] methods)
        {
            Scatterplot[] scatterplots = new Scatterplot[methods.Length];

            for (int currentMethodID = 0; currentMethodID < methods.Length; currentMethodID++)
            {
                DataTable dt = ((DataTable)StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 3).GetStoredObject());

                double[] table = dt.Columns[dt.Columns.Count - 1].ToArray<double>();
                // Get the expected output labels (last column)
                int[] expected = table.ToInt32();

                double[] actual = StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 4).GetStored1DArray().ToDouble();

                scatterplots[currentMethodID] = new PaintingHeplers().CreateReceiverOperatingCharacteristicCurve(actual, expected);

                scatterplotViewROCCurve.Graph.GraphPane.AddCurve(methods[currentMethodID].name, scatterplots[currentMethodID].XAxis,
                    scatterplots[currentMethodID].YAxis, colors[currentMethodID]);
            }
        }

        void CreateHistogramPicture(AssoocArray[] methods, System.Windows.Forms.DataVisualization.Charting.Chart chart, double[] pointsArray)
        {
            string seriesName = "Methods";
            // Set palette.
            chart.Palette = ChartColorPalette.SeaGreen;

            // Set title.
            chart.Titles.Add("Методы анализа");

            ChartArea chartArea = new ChartArea("Area1");
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Maximum = methods.Length + 1;
            chartArea.AxisY.Minimum = 0;
            chartArea.AxisY.Maximum = pointsArray.Max<double>() > 1 ? pointsArray.Max<double>() : 1;

            chartArea.AxisX.Interval = 1;
            chartArea.AxisY.Interval = pointsArray.Max<double>() > 5 ? pointsArray.Max<double>() / 4 : pointsArray.Max<double>() > 1 ? 1 : 0.25f;
            chart.ChartAreas.Add(chartArea);

            chart.Series.Add(seriesName);

            // Add series.
            for (int i = 0; i < pointsArray.Length; i++)
            {
                chart.Series[seriesName].ChartType = SeriesChartType.Column;

                // Add point
                chart.Series[seriesName].Points.Add(pointsArray[i]);
                chart.Series[seriesName].Points[i].Label = pointsArray[i].ToString();
                chart.Series[seriesName].Points[i].AxisLabel = methods[i].name;
                chart.Series[seriesName].Points[i].LegendText = methods[i].name;

                chart.Series[seriesName].Points[i].Color = colors[i];
            }

            chart.Update();
        }
    }
}
