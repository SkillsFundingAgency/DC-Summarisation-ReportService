using ESFA.DC.Summarisation.ReportService.Constants;
using ESFA.DC.Summarisation.ReportService.Interface;
using ESFA.DC.Summarisation.ReportService.Interface.Model;

namespace ESFA.DC.Summarisation.ReportService.Service.CollectionTypeFormatters
{
    public class APPSCollectionTypeFormatter : ICollectionTypeFormatter
    {
        public string CollectionReturnCode => ContextConstants.CollectionReturnCodeApp;

        public CollectionTypeDetails SetupType(int collectionYear, string collectionReturn)
        {
            return new CollectionTypeDetails($"APPS", collectionReturn);
        }
    }
}