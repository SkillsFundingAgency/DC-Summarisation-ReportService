using ESFA.DC.Summarisation.ReportService.Constants;
using ESFA.DC.Summarisation.ReportService.Interface;
using ESFA.DC.Summarisation.ReportService.Interface.Model;

namespace ESFA.DC.Summarisation.ReportService.Service.CollectionTypeFormatters
{
    public class ILRCollectionTypeFormatter : ICollectionTypeFormatter
    {
        private readonly IFileNameService _fileNameService;

        public ILRCollectionTypeFormatter(IFileNameService fileNameService)
        {
            _fileNameService = fileNameService;
        }

        public string CollectionReturnCode => ContextConstants.CollectionReturnCodeDC;

        public CollectionTypeDetails SetupType(int collectionYear, string collectionReturn)
        {
            var reportBaseName = "Data Extract Report";
            var fileName = _fileNameService.Generate(reportBaseName, collectionReturn);

            return new CollectionTypeDetails($"ILR{collectionYear:D4}", collectionReturn, fileName);
        }
    }
}