using Accord.Math;
using BigDataAnalyzer.Storage;
using System;
using System.Data;
using Accord.Neuro;
using Accord.Neuro.Learning;
using Accord;
using BigDataAnalyzer.Painting;

namespace BigDataAnalyzer.Teaching
{
    /// <summary>
    /// This class implement a Desicion Tree teaching method
    /// </summary>
    public class AITeachingClassMethodNeuralNetwork : IAITeachingMethod
    {
        int currentMethodID;
        int storageType;
        int assocArrayCapacity;
        int[] indexesOfColumns; // Indexes of selected columns. Last element is always classifier

        public AITeachingClassMethodNeuralNetwork(int currentMethodID, int storageType, int assocArrayCapacity, int[] indexesOfColumns)
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

            // number of learning samples
            int samples = table.GetLength(0);

            // prepare learning data
            double[][] inputs = table.GetColumns(indexesOfColumns[0], indexesOfColumns[1]).ToJagged();
            double[][] outputs = table.GetColumn(indexesOfColumns[indexesOfColumns.Length - 1]).Transpose().ToJagged();

            double error;
            double errorLimit = 0.001;

            // In our problem, we have 2 inputs (x, y pairs), and we will 
            // be creating a network with 5 hidden neurons and 1 output
            ActivationNetwork network = new ActivationNetwork(new RectifiedLinearFunction(),
                inputsCount: 2, neuronsCount: new[] { 5, 1 });

            // create teacher
            LevenbergMarquardtLearning teacher = new LevenbergMarquardtLearning(network);

            // set learning rate and momentum
            teacher.LearningRate = 0.1;

            // iterations
            int iteration = 1;
            int iterations = 35;

            DoubleRange[] ranges = table.GetRange(0);

            double matrixStepSize = new PaintingHeplers().CalculateGraphStepSize(ranges);

            double[][] map = Matrix.Cartesian(
                Vector.Range(ranges[0], matrixStepSize),
                Vector.Range(ranges[1], matrixStepSize));

            bool needToStop = false;

            // loop
            while (!needToStop)
            {
                // run epoch of learning procedure
                error = teacher.RunEpoch(inputs, outputs) / samples;

                var result = map.Apply(network.Compute).GetColumn(0).Apply(Math.Sign);

                if (error < errorLimit)
                    break;

                // increase current iteration
                iteration++;

                // check if we need to stop
                if ((iterations != 0) && (iteration > iterations))
                    break;
            }

            if (AIStorageList.getInstanse(assocArrayCapacity).GetAIStorageObject(currentMethodID) == null)
            {
                AIStorageList.getInstanse(assocArrayCapacity).SetAIStorageObject(network, currentMethodID);
            }
            else
            {
                network = (ActivationNetwork)AIStorageList.getInstanse(assocArrayCapacity).GetAIStorageObject(currentMethodID);
            }

            // Save changes to storage
            AIStorageList.getInstanse(assocArrayCapacity).SetAIStorageObject(network, currentMethodID);

            storageObject.teached = true;
        }
    }
}

