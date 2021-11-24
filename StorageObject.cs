namespace BigDataAnalyzer.Storage
{
    /// <summary>
    /// Anstract Object for all storajes in program
    /// </summary>
    public class StorageObject
    {
        public int methodID; // method ID
        public int storageType; // Storage type (Learn, Analyze, Result, etc.)
        object[] storedObject1DArray;
        object storedObject;
        object storedGraphObject; // ZedGraph Storage type (1 - learn, 2 - learned, 3 - analyzed, etc.)
        object storedConfusionMatrix;
        public bool teached;

        public StorageObject(int methodID, int storageType)
        {
            this.methodID = methodID;
            this.storageType = storageType;
            this.teached = false;
        }

        public object GetStoredConfusionMatrix()
        {
            return storedConfusionMatrix;
        }

        public void SetStoredConfusionMatrix(object objectToStore)
        {
            storedConfusionMatrix = objectToStore;
        }

        public object GetStoredGraphObject()
        {
            return storedGraphObject;
        }

        public void SetStoredGraphObject(object objectToStore)
        {
            storedGraphObject = objectToStore;
        }

        public object[] GetStored1DArray()
        {
            return storedObject1DArray;
        }

        public void SetStored1DArray(object[] objectToStore)
        {
            storedObject1DArray = objectToStore;
        }

        public object GetStoredObject()
        {
            return storedObject;
        }

        public void SetStoredObject(object objectToStore)
        {
            storedObject = objectToStore;
        }
    }
}
