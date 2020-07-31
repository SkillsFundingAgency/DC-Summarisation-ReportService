namespace ESFA.DC.Summarisation.ReportService.Interface.Model
{
    public class CollectionTypeDetails
    {
        public CollectionTypeDetails(string collectionType, string collectionReturn, string fileName = null)
        {
            CollectionType = collectionType;
            CollectionReturn = collectionReturn;
            FileName = fileName;
        }

        public string CollectionType { get; }  
        public string CollectionReturn { get; }
        public string FileName { get; }
    }
}