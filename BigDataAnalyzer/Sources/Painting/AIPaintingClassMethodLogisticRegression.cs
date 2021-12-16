using Accord;
using Accord.Math;
using BigDataAnalyzer.Storage;
using System;
using System.Windows.Forms;
using Accord.Controls;
using System.Data;
using ZedGraph;
using System.Drawing;
using Accord.Statistics.Analysis;
using Accord.Statistics.Models.Regression;
using BigDataAnalyzer.Forms;

namespace BigDataAnalyzer.Painting
{
    /// <summary>
    /// This class implement a Desicion Tree painting method
    /// </summary>
    public class AIPaintingClassMethodLogisticRegression : IAIPaintingMethod
    {
        int currentMethodID;
        int storageType;
        int assocArrayCapacity;
        object[] objectsToPaint;
        int[] indexesOfColumns; // Indexes of selected columns. Last element is always classifier

        public AIPaintingClassMethodLogisticRegression(int currentMethodID, int storageType, int assocArrayCapacity, object[] objectsToPaint, int[] indexesOfColumns)
        {
            this.currentMethodID = currentMethodID;
            this.storageType = storageType;
            this.assocArrayCapacity = assocArrayCapacity;
            this.objectsToPaint = objectsToPaint;
            this.indexesOfColumns = indexesOfColumns;
        }

        public void PaintLearnData()
        {
            string[] columnNames;
            ZedGraphControl graph = new ZedGraphControl();
            StorageObject storageObject = StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, storageType);
            double[,] sourceMatrix = ((DataTable)storageObject.GetStoredObject()).ToMatrix(out columnNames);

            new PaintingHeplers().CreateScatterPlot(graph, sourceMatrix, columnNames, indexesOfColumns);

            StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, storageType).SetStoredGraphObject(graph);
        }

        public void PaintLearnResult()
        {
            string[] columnNames;
            ZedGraphControl graphTeachingResult = new ZedGraphControl();

            StorageObject storageObject = StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 1);

            // Creates a matrix from the entire source data table
            double[,] table = ((DataTable)storageObject.GetStoredObject()).ToMatrix(out columnNames);

            // Get the ranges for each variable (X and Y)
            DoubleRange[] ranges = table.GetRange(0);

            double matrixStepSize = new PaintingHeplers().CalculateGraphStepSize(ranges);

            // Generate a Cartesian coordinate system
            double[][] map = Matrix.Cartesian(
                Vector.Range(ranges[0], matrixStepSize),
                Vector.Range(ranges[1], matrixStepSize));

            // Classify each point in the Cartesian coordinate system
            double[,] surface = map.ToMatrix().InsertColumn(
                ((LogisticRegression)AIStorageList.getInstanse(assocArrayCapacity).GetAIStorageObject(currentMethodID)).Decide(map));

            new PaintingHeplers().CreateTeachingResultScatterPlot(graphTeachingResult, surface, columnNames);

            StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 2).SetStoredGraphObject(graphTeachingResult);
        }

        public void PaintAnalysed()
        {
            string[] columnNames;
            ZedGraphControl graphTesting = new ZedGraphControl();

            DataTable dt = (DataTable)StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 3).GetStoredObject();

            // Extract a column names from DataTable
            dt.ToMatrix(out columnNames);

            // Creates a matrix from the entire source data table
            double[][] table = dt.ToJagged();

            // Get only the input vector values (first two columns)
            double[][] inputs = table.GetColumns(indexesOfColumns[0], indexesOfColumns[1]);

            // Get the expected output labels (last column)
            int[] expected = table.GetColumn(indexesOfColumns[indexesOfColumns.Length - 1]).ToInt32();

            int[] actual = StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 4).GetStored1DArray().ToInt32();

            // Use confusion matrix to compute some statistics.
            ConfusionMatrix confusionMatrix = new ConfusionMatrix(actual, expected, 1, 0);
            StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 4).SetStoredConfusionMatrix(confusionMatrix);

            // Create performance scatter plot
            new PaintingHeplers().CreateResultScatterPlot(graphTesting, inputs, expected.ToDouble(), actual.ToDouble(), columnNames);

            StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, 4).SetStoredGraphObject(graphTesting);
        }
    }
}
