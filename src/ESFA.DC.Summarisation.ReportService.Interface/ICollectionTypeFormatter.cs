using ESFA.DC.Summarisation.ReportService.Interface.Model;

namespace ESFA.DC.Summarisation.ReportService.Interface
{
    public interface ICollectionTypeFormatter
    {
        string CollectionReturnCode { get; }

        CollectionTypeDetails SetupType(int collectionYear, string collectionReturn);
    }
}