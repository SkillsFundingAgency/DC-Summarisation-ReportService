using ESFA.DC.Summarisation.ReportService.Constants;
using ESFA.DC.Summarisation.ReportService.Interface;
using ESFA.DC.Summarisation.ReportService.Interface.Model;

namespace ESFA.DC.Summarisation.ReportService.Service.CollectionTypeFormatters
{
    public class NCSCollectionTypeFormatter : ICollectionTypeFormatter
    {
        private readonly IFileNameService _fileNameService;

        public NCSCollectionTypeFormatter(IFileNameService fileNameService)
        {
            _fileNameService = fileNameService;
        }

        public string CollectionReturnCode => ContextConstants.CollectionReturnCodeNCS;

        public CollectionTypeDetails SetupType(int collectionYear, string collectionReturn)
        {
            var reportBaseName = "NCS Summarised Actuals Data Extract";
            var fileName = _fileNameService.Generate(reportBaseName, collectionReturn);

            return new CollectionTypeDetails($"NCS{collectionYear:D4}", collectionReturn, fileName);
        }
    }
}