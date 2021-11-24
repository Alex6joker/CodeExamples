using System.Collections.Generic;

namespace BigDataAnalyzer.Storage
{
    /// <summary>
    /// Class implement a link between working classes (such as Teaching class, Analyzing, Result form, etc.) and storages in program
    /// Only one getter, no more needed
    /// </summary>
    public class StorageObjectGetter
    {
        List<StorageObject> storageObjects; // List of storages

        static StorageObjectGetter instanse; // this class instance for singleton pattern

        public StorageObjectGetter()
        {
            storageObjects = new List<StorageObject>();
        }

        /// <summary>
        /// Singleton implementation function
        /// </summary>
        /// <returns></returns>
        public static StorageObjectGetter getInstanse()
        {
            if (instanse == null)
            {
                instanse = new StorageObjectGetter();
            }
            return instanse;
        }

        /// <summary>
        /// Get instanse of desired storage
        /// </summary>
        /// <param name="methodID"></param>
        /// <returns></returns>
        public StorageObject GetStorageByMethodID(int methodID, int storageType)
        {
            // First check the existing list
            foreach (StorageObject storageObject in storageObjects)
            {
                if (storageObject.methodID == methodID && storageObject.storageType == storageType)
                {
                    return storageObject;
                }
            }

            // if not-exist create new one
            return CreateDesiredStorageType(methodID, storageType);
        }

        /// <summary>
        /// Creating a new storage of desired type
        /// Some implenetation of simple factory method too
        /// </summary>
        /// <param name="methodID"></param>
        /// <param name="storageType"></param>
        /// <returns></returns>
        StorageObject CreateDesiredStorageType(int methodID, int storageType)
        {
            StorageObject storageObj = new StorageObject(methodID, storageType);

            storageObjects.Add(storageObj);

            return storageObj;
        }
    }
}
