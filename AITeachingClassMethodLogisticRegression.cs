using Accord.Math;
using BigDataAnalyzer.Storage;
using System.Data;
using Accord.Statistics.Models.Regression.Fitting;
using Accord.Statistics.Models.Regression;

namespace BigDataAnalyzer.Teaching
{
    /// <summary>
    /// This class implement a Desicion Tree teaching method
    /// </summary>
    public class AITeachingClassMethodLogisticRegression : IAITeachingMethod
    {
        int currentMethodID;
        int storageType;
        int assocArrayCapacity;
        int[] indexesOfColumns; // Indexes of selected columns. Last element is always classifier

        public AITeachingClassMethodLogisticRegression(int currentMethodID, int storageType, int assocArrayCapacity, int[] indexesOfColumns)
        {
            this.currentMethodID = currentMethodID;
            this.storageType = storageType;
            this.assocArrayCapacity = assocArrayCapacity;
            this.indexesOfColumns = indexesOfColumns;
        }

        public void TeachAI()
        {
            StorageObject storageObject = StorageObjectGetter.getInstanse().GetStorageByMethodID(currentMethodID, storageType);

            // Creates a matrix from the entire source data table
            double[,] table = ((DataTable)storageObject.GetStoredObject()).ToMatrix();

            // Get only the input vector values (first two columns)
            double[][] inputs = table.GetColumns(indexesOfColumns[0], indexesOfColumns[1]).ToJagged();

            // Get only the output labels (last column)
            int[] outputs = table.GetColumn(indexesOfColumns[indexesOfColumns.Length - 1]).ToInt32();

            // Create iterative re-weighted least squares for logistic regressions
            var teacher = new IterativeReweightedLeastSquares<LogisticRegression>()
            {
                MaxIterations = 100,
                Regularization = 1e-6
            };

            // Create new or use existing
            LogisticRegression lr = null;

            if (AIStorageList.getInstanse(assocArrayCapacity).GetAIStorageObject(currentMethodID) == null)
            {
                AIStorageList.getInstanse(assocArrayCapacity).SetAIStorageObject(lr, currentMethodID);
            }
            else
            {
                lr = (LogisticRegression)AIStorageList.getInstanse(assocArrayCapacity).GetAIStorageObject(currentMethodID);
            }

            // Use the teacher algorithm to learn the regression:
            lr = teacher.Learn(inputs, outputs);

            // Save changes to storage
            AIStorageList.getInstanse(assocArrayCapacity).SetAIStorageObject(lr, currentMethodID);

            storageObject.teached = true;
        }
    }
}
