using Accord;
using Accord.Statistics.Analysis;
using Accord.Statistics.Visualizations;
using System;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace BigDataAnalyzer.Painting
{
    public class PaintingHeplers
    {
        /// <summary>
        /// Creating a plot
        /// </summary>
        /// <param name="zgc"></param>
        /// <param name="graph"></param>
        public void CreateScatterPlot(ZedGraphControl zgc, double[,] graph, string[] columnNames, int[] indexesOfColumns)
        {
            GraphPane myPane = zgc.GraphPane;
            myPane.CurveList.Clear();

            // Set the titles
            myPane.Title.IsVisible = false;
            myPane.XAxis.Title.Text = "Axis X";//columnNames[0];
            myPane.YAxis.Title.Text = "Axis Y";//columnNames[1];

            // Classification problem
            PointPairList list1 = new PointPairList(); // Z = 0
            PointPairList list2 = new PointPairList(); // Z = 1
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                if (graph[i, graph.GetLength(1) - 1] <= 0)
                    list1.Add(graph[i, indexesOfColumns[0]], graph[i, indexesOfColumns[1]]);
                if (graph[i, graph.GetLength(1) - 1] == 1)
                    list2.Add(graph[i, indexesOfColumns[0]], graph[i, indexesOfColumns[1]]);
            }

            // Add the curve
            LineItem myCurve = myPane.AddCurve("Class 1", list1, Color.Blue, SymbolType.Square);
            myCurve.Line.IsVisible = false;
            myCurve.Symbol.Border.IsVisible = false;
            myCurve.Symbol.Fill = new Fill(Color.Blue);

            myCurve = myPane.AddCurve("Class 2", list2, Color.Green, SymbolType.Square);
            myCurve.Line.IsVisible = false;
            myCurve.Symbol.Border.IsVisible = false;
            myCurve.Symbol.Fill = new Fill(Color.Green);

            // Fill the background of the chart rect and pane
            //myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45.0f);
            //myPane.Fill = new Fill(Color.White, Color.SlateGray, 45.0f);
            myPane.Fill = new Fill(Color.WhiteSmoke);

            zgc.AxisChange();
            zgc.Invalidate();
        }

        /// <summary>
        /// Creating a plot
        /// </summary>
        /// <param name="zgc"></param>
        /// <param name="graph"></param>
        public void CreateTeachingResultScatterPlot(ZedGraphControl zgc, double[,] graph, string[] columnNames)
        {
            GraphPane myPane = zgc.GraphPane;
            myPane.CurveList.Clear();

            // Set the titles
            myPane.Title.IsVisible = false;
            myPane.XAxis.Title.Text = "Axis X";//columnNames[0];
            myPane.YAxis.Title.Text = "Axis Y";//columnNames[1];

            // Classification problem
            PointPairList list1 = new PointPairList(); // Z = 0
            PointPairList list2 = new PointPairList(); // Z = 1
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                if (graph[i, graph.GetLength(1) - 1] <= 0)
                    list1.Add(graph[i, 0], graph[i, 1]);
                if (graph[i, graph.GetLength(1) - 1] == 1)
                    list2.Add(graph[i, 0], graph[i, 1]);
            }

            // Add the curve
            LineItem myCurve = myPane.AddCurve("Class 1", list1, Color.Blue, SymbolType.Square);
            myCurve.Line.IsVisible = false;
            myCurve.Symbol.Border.IsVisible = false;
            myCurve.Symbol.Fill = new Fill(Color.Blue);

            myCurve = myPane.AddCurve("Class 2", list2, Color.Green, SymbolType.Square);
            myCurve.Line.IsVisible = false;
            myCurve.Symbol.Border.IsVisible = false;
            myCurve.Symbol.Fill = new Fill(Color.Green);


            // Fill the background of the chart rect and pane
            //myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45.0f);
            //myPane.Fill = new Fill(Color.White, Color.SlateGray, 45.0f);
            myPane.Fill = new Fill(Color.WhiteSmoke);

            zgc.AxisChange();
            zgc.Invalidate();
        }

        /// <summary>
        /// Creating a result plot
        /// </summary>
        /// <param name="zgc"></param>
        /// <param name="inputs"></param>
        /// <param name="expected"></param>
        /// <param name="output"></param>
        public void CreateResultScatterPlot(ZedGraphControl zgc, double[][] inputs, double[] expected, double[] output, string[] columnNames)
        {
            GraphPane myPane = zgc.GraphPane;
            myPane.CurveList.Clear();

            // Set the titles
            myPane.Title.IsVisible = false;
            myPane.XAxis.Title.Text = columnNames[0];
            myPane.YAxis.Title.Text = columnNames[1];



            // Classification problem
            PointPairList list1 = new PointPairList(); // Z = 0, OK
            PointPairList list2 = new PointPairList(); // Z = 1, OK
            PointPairList list3 = new PointPairList(); // Z = 0, Error
            PointPairList list4 = new PointPairList(); // Z = 1, Error
            for (int i = 0; i < output.Length; i++)
            {
                if (output[i] == 0)
                {
                    if (expected[i] == 0)
                        list1.Add(inputs[i][0], inputs[i][1]);
                    if (expected[i] == 1)
                        list3.Add(inputs[i][0], inputs[i][1]);
                }
                else
                {
                    if (expected[i] == 0)
                        list4.Add(inputs[i][0], inputs[i][1]);
                    if (expected[i] == 1)
                        list2.Add(inputs[i][0], inputs[i][1]);
                }
            }

            // Add the curve
            LineItem
            myCurve = myPane.AddCurve("G1 Hits", list1, Color.Blue, SymbolType.Diamond);
            myCurve.Line.IsVisible = false;
            myCurve.Symbol.Border.IsVisible = false;
            myCurve.Symbol.Fill = new Fill(Color.Blue);

            myCurve = myPane.AddCurve("G2 Hits", list2, Color.Green, SymbolType.Diamond);
            myCurve.Line.IsVisible = false;
            myCurve.Symbol.Border.IsVisible = false;
            myCurve.Symbol.Fill = new Fill(Color.Green);

            myCurve = myPane.AddCurve("G1 Miss", list3, Color.Blue, SymbolType.Plus);
            myCurve.Line.IsVisible = false;
            myCurve.Symbol.Border.IsVisible = true;
            myCurve.Symbol.Fill = new Fill(Color.Blue);

            myCurve = myPane.AddCurve("G2 Miss", list4, Color.Green, SymbolType.Plus);
            myCurve.Line.IsVisible = false;
            myCurve.Symbol.Border.IsVisible = true;
            myCurve.Symbol.Fill = new Fill(Color.Green);


            // Fill the background of the chart rect and pane
            //myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45.0f);
            //myPane.Fill = new Fill(Color.White, Color.SlateGray, 45.0f);
            myPane.Fill = new Fill(Color.WhiteSmoke);

            zgc.AxisChange();
            zgc.Invalidate();
        }

        public void RefreshGraph(ZedGraphControl graph, ZedGraphControl newGraph)
        {
            try
            {
                RectangleF rectangle = graph.GraphPane.Rect;
                graph.GraphPane = newGraph.GraphPane.Clone();

                graph.GraphPane.Rect = rectangle;
                // Вызываем метод AxisChange (), чтобы обновить данные об осях.
                graph.AxisChange();// Обновляем график
                graph.Invalidate();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Scatterplot CreateReceiverOperatingCharacteristicCurve(double[] real, int[] expected)
        {
            ReceiverOperatingCharacteristic rocCurve = new ReceiverOperatingCharacteristic(expected, real);
            rocCurve.Compute(0.05f);

            return rocCurve.GetScatterplot(false);
        }

        // Calculate a step size to paint graph continiously 
        public double CalculateGraphStepSize(DoubleRange[] ranges)
        {
            double[] XYranges = { Math.Abs(ranges[0].Min) + Math.Abs(ranges[0].Max), Math.Abs(ranges[1].Min) + Math.Abs(ranges[1].Max) };
            double maxRange = XYranges[0] > XYranges[1] ? XYranges[0] : XYranges[1];

            return maxRange / 75;
        }
    }
}
