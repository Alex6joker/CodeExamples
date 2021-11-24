using Accord.Math;
using Accord.Neuro;
using Accord.Statistics;
using BigDataAnalyzer.Storage;
using System.Data;

namespace BigDataAnalyzer.Analyzing
{
    /// <summary>
    /// This class implement a Desicion Tree analyzing method
    /// </summary>
    public class AIAnalyzingClassMethodNeuralNetwork : IAIAnalyzingMethod
    {
        int methodID;
        int storageType;
        int assocArrayCapacity;
        int resultStorageType;
        int[] indexesOfColumns; // Indexes of selected columns. Last element is always classifier

        public AIAnalyzingClassMethodNeuralNetwork(int methodID, int storageType, int assocArrayCapacity, int[] indexesOfColumns)
        {
            this.methodID = methodID;
            this.storageType = storageType;
            this.assocArrayCapacity = assocArrayCapacity;
            this.resultStorageType = 4;
            this.indexesOfColumns = indexesOfColumns;
        }

        public void Analyze()
        {
            StorageObject storageObject = StorageObjectGetter.getInstanse().GetStorageByMethodID(methodID, storageType);

            // Creates a matrix from the entire source data table
            double[][] table = ((DataTable)storageObject.GetStoredObject()).ToJagged();

            // Get only the input vector values (first two columns)
            double[][] inputs = table.GetColumns(indexesOfColumns[0], indexesOfColumns[1]);

            // Classify the samples using the model
            int[] answers = inputs.Apply(((ActivationNetwork)AIStorageList.getInstanse(assocArrayCapacity).GetAIStorageObject(methodID)).Compute).GetColumn(0).Apply(System.Math.Sign);

            StorageObjectGetter.getInstanse().GetStorageByMethodID(methodID, resultStorageType).SetStored1DArray(answers.ToObject());
        }
    }
}


