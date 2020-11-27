using ESFA.DC.Summarisation.ReportService.Constants;
using ESFA.DC.Summarisation.ReportService.Interface;
using ESFA.DC.Summarisation.ReportService.Interface.Model;

namespace ESFA.DC.Summarisation.ReportService.Service.CollectionTypeFormatters
{
    public class ESFCollectionTypeFormatter : ICollectionTypeFormatter
    {
        public string CollectionReturnCode => ContextConstants.CollectionReturnCodeESF;

        public CollectionTypeDetails SetupType(int collectionYear, string collectionReturn)
        {
            return new CollectionTypeDetails($"ESF", collectionReturn);
        }
    }
}