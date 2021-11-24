using System.Windows.Forms;

namespace BigDataAnalyzer.Painting
{
    /// <summary>
    /// Class with abstract implementation of painting for each analyzing method
    /// </summary>
    public class AIPaintingClass
    {
        int methodID;
        int storageType;
        int assocArrayCapacity;
        object[] objectsToPaint;
        int operationType;
        int[] indexesOfColumns; // Indexes of selected columns. Last element is always classifier

        IAIPaintingMethod PaintingStrategy; // Context of strategy    

        public AIPaintingClass(int methodID, int storageType, int assocArrayCapacity, int operationType, object[] objectsToPaint, int[] indexesOfColumns)
        {
            this.methodID = methodID;
            this.storageType = storageType;
            this.assocArrayCapacity = assocArrayCapacity;
            this.objectsToPaint = objectsToPaint;
            this.operationType = operationType;
            this.indexesOfColumns = indexesOfColumns;
        }

        /// <summary>
        /// Choose right strategy (settr)
        /// </summary>
        void ChooseCorrectAITEachingMethod()
        {
            if (methodID == 0)
            {
                PaintingStrategy = new AIPaintingClassMethodDecisionTree(methodID, storageType, assocArrayCapacity, objectsToPaint, indexesOfColumns);
            }
            else if (methodID == 1)
            {
                PaintingStrategy = new AIPaintingClassMethodLogisticRegression(methodID, storageType, assocArrayCapacity, objectsToPaint, indexesOfColumns);
            }
            else if (methodID == 2)
            {
                PaintingStrategy = new AIPaintingClassMethodNeuralNetwork(methodID, storageType, assocArrayCapacity, objectsToPaint, indexesOfColumns);
            }
        }

        public void StartPaint()
        {
            ChooseCorrectAITEachingMethod();
            if(PaintingStrategy != null)
            {
                if (operationType == 1)
                {
                    PaintingStrategy.PaintLearnData();
                }
                else if (operationType == 2)
                {
                    PaintingStrategy.PaintLearnResult();
                }
                else if (operationType == 3)
                {
                    PaintingStrategy.PaintAnalysed();
                }
            }
            else
            {
                MessageBox.Show("There is no painting method for this AI method");
            }
        }
    }
}