using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.Math;
using BigDataAnalyzer.Storage;
using System;
using System.Data;
using System.Windows.Forms;

namespace BigDataAnalyzer.Teaching
{
    /// <summary>
    /// This class implement a Desicion Tree teaching method
    /// </summary>
    public class AITeachingClassMethodDecisionTree : IAITeachingMethod
    {
        int currentMethodID;
        int storageType;
        int assocArrayCapacity;
        int[] indexesOfColumns; // Indexes of selected columns. Last element is always classifier

        public AITeachingClassMethodDecisionTree(int currentMethodID, int storageType, int assocArrayCapacity, int[] indexesOfColumns)
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


            // Specify the input variables
            DecisionVariable[] variables =
            {
                new DecisionVariable("x", DecisionVariableKind.Continuous),
                new DecisionVariable("y", DecisionVariableKind.Continuous),
            };

            // Create the C4.5 learning algorithm
            var c45 = new C45Learning(variables);

            // Create new tree or use existing
            DecisionTree tree = null;

            if(AIStorageList.getInstanse(assocArrayCapacity).GetAIStorageObject(currentMethodID) == null)
            {
                AIStorageList.getInstanse(assocArrayCapacity).SetAIStorageObject(tree, currentMethodID);
            }
            else
            {
                tree = (DecisionTree)AIStorageList.getInstanse(assocArrayCapacity).GetAIStorageObject(currentMethodID);
            }
            
            try
            {
                // Learn the decision tree using C4.5
                tree = c45.Learn(inputs, outputs);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Ошибка обучения модели" + Environment.NewLine + ex.Message);
            }

            // Save changes to storage
            AIStorageList.getInstanse(assocArrayCapacity).SetAIStorageObject(tree, currentMethodID);

            storageObject.teached = true;
        }
    }
}
