using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.Summarisation.ReportService.Interface;

namespace ESFA.DC.Summarisation.ReportService.Service
{
    public class FileNameService : IFileNameService
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public FileNameService(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public string Generate(string reportBaseName, string period)
        {
            return $"{period}/{reportBaseName} {period} {GetCurrentDateTime}.csv";
        }

        public string GetCurrentDateTime => $"{_dateTimeProvider.GetNowUtc():yyyyMMdd-HHmmss}";
    }
}
