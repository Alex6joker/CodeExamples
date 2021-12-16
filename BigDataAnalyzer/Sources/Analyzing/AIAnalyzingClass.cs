using System;
using System.Windows.Forms;

namespace BigDataAnalyzer.Analyzing
{
    /// <summary>
    /// Class with abstract implementation of analyzing for each analyzing method
    /// </summary>
    public class AIAnalyzingClass
    {
        int methodID;
        int storageType;
        int assocArrayCapacity;
        int[] indexesOfColumns; // Indexes of selected columns. Last element is always classifier

        IAIAnalyzingMethod AnalyzingStrategy; // Context of strategy    

        public AIAnalyzingClass(int methodID, int storageType, int assocArrayCapacity, int[] indexesOfColumns)
        {
            this.methodID = methodID;
            this.storageType = storageType;
            this.assocArrayCapacity = assocArrayCapacity;
            this.indexesOfColumns = indexesOfColumns;
        }

        /// <summary>
        /// Choose right strategy (settr)
        /// </summary>
        void ChooseCorrectAITEachingMethod()
        {
            if (methodID == 0)
            {
                AnalyzingStrategy = new AIAnalyzingClassMethodDesicionTree(methodID, storageType, assocArrayCapacity, indexesOfColumns);
            }
            else if(methodID == 1)
            {
                AnalyzingStrategy = new AIAnalyzingClassMethodLogisticRegression(methodID, storageType, assocArrayCapacity, indexesOfColumns);
            }
            else if (methodID == 2)
            {
                AnalyzingStrategy = new AIAnalyzingClassMethodNeuralNetwork(methodID, storageType, assocArrayCapacity, indexesOfColumns);
            }
        }

        public void StartAnalyzingAI()
        {
            ChooseCorrectAITEachingMethod();
            if (AnalyzingStrategy != null)
            {
                AnalyzingStrategy.Analyze();
            }
            else
            {
                MessageBox.Show("There is no analysing method for this AI method");
            }
        }
    }
}
