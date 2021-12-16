using System.Collections.Generic;

namespace BigDataAnalyzer.Storage
{
    /// <summary>
    /// Class is responsible for storaging an AI class objects
    /// </summary>
    public class AIStorageList
    {
        List<object> AIObjects;

        static AIStorageList instanse; // this class instance for singleton pattern

        public AIStorageList(int assocArrayCapacity)
        {
            AIObjects = new List<object>(assocArrayCapacity);

            for(int i = 0; i < assocArrayCapacity; i++)
            {
                AIObjects.Add(null);
            }
        }

        /// <summary>
        /// Singleton implementation function
        /// </summary>
        /// <returns></returns>
        public static AIStorageList getInstanse(int assocArrayCapacity)
        {
            if (instanse == null)
            {
                instanse = new AIStorageList(assocArrayCapacity);
            }
            return instanse;
        }

        public object GetAIStorageObject(int indexOnAssocArray)
        {
            return AIObjects[indexOnAssocArray];
        }

        public void SetAIStorageObject(object objAI, int indexOnAssocArray)
        {
            AIObjects[indexOnAssocArray] = objAI;
        }
    }
}
