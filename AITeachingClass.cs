using Accord.Controls;
using BigDataAnalyzer.UI;
using System;
using System.Windows.Forms;

namespace BigDataAnalyzer.Teaching
{
    /// <summary>
    /// Class with abstract implementation of teaching for each analyzing method
    /// </summary>
    public class AITeachingClass
    {
        int methodID;
        int storageType;
        int assocArrayCapacity;
        int[] indexesOfColumns; // Indexes of selected columns. Last element is always classifier

        IAITeachingMethod TeachingStrategy; // Context of strategy    
        
        public AITeachingClass(int methodID, int storageType, int assocArrayCapacity, int[] indexesOfColumns)
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
            if(methodID == 0)
            {
                TeachingStrategy = new AITeachingClassMethodDecisionTree(methodID, storageType, assocArrayCapacity, indexesOfColumns);
            }
            else if (methodID == 1)
            {
                TeachingStrategy = new AITeachingClassMethodLogisticRegression(methodID, storageType, assocArrayCapacity, indexesOfColumns);
            }
            else if (methodID == 2)
            {
                TeachingStrategy = new AITeachingClassMethodNeuralNetwork(methodID, storageType, assocArrayCapacity, indexesOfColumns);
            }
        }

        public void StartTeachAI()
        {
            ChooseCorrectAITEachingMethod();
            if(TeachingStrategy != null)
            {
                TeachingStrategy.TeachAI();
            }
            else
            {
                MessageBox.Show("There is no teaching method for this AI method");
            }
        }        
    }
}
