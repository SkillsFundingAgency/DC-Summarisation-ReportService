using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.CsvService.Interface;
using ESFA.DC.Logging.Interfaces;
using ESFA.DC.Summarisation.ReportService.Data.Interface;
using ESFA.DC.Summarisation.ReportService.Interface;
using ESFA.DC.Summarisation.ReportService.Interface.Model;
using ESFA.DC.Summarisation.ReportService.Report.Constants;

namespace ESFA.DC.Summarisation.ReportService.Report.PeriodSummary
{
    public class PeriodSummaryReport : IReport
    {
        private readonly IFileNameService _fileNameService;
        private readonly ICsvFileService _csvService;
        private readonly IEnumerable<ICollectionTypeFormatter> _collectionTypeFormatters;
        private readonly IPeriodSummaryDataProvider _periodSummaryDataProvider;
        private readonly ILogger _logger;

        public PeriodSummaryReport(
            IFileNameService fileNameService,
            ICsvFileService csvService,
            IEnumerable<ICollectionTypeFormatter> collectionTypeFormatters,
            IPeriodSummaryDataProvider periodSummaryDataProvider,
            ILogger logger
            )
        {
            _fileNameService = fileNameService;
            _csvService = csvService;
            _collectionTypeFormatters = collectionTypeFormatters;
            _periodSummaryDataProvider = periodSummaryDataProvider;
            _logger = logger;
        }

        public string TaskName => ReportTaskNameConstants.PeriodSummaryReport;

        public async Task<IEnumerable<string>> GenerateAsync(IReportServiceContext reportServiceContext, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Report Generation for Period Summary started");

            var collectionTypes = GetCollectionTypes(reportServiceContext);

            var fileName = collectionTypes.First(ct => ct.FileName != null).FileName;

            var logSum = string.Join(" and ", collectionTypes.Select(ct => $"{ct.CollectionType} / {ct.CollectionReturn}"));
            _logger.LogInfo($"Retrieving summarised data for {logSum}");

            var periods = await _periodSummaryDataProvider.ProvideAsync(collectionTypes, cancellationToken);
            
            _logger.LogInfo($"Saving summarised data as '{fileName}' in {reportServiceContext.Container}");
            await _csvService.WriteAsync<Model.PeriodSummary, PeriodSummaryReportClassMap>(periods, fileName, reportServiceContext.Container, cancellationToken);

            _logger.LogInfo("Report Generation for Period Summary finished");
            return new[] { fileName };
        }

        public List<CollectionTypeDetails> GetCollectionTypes(IReportServiceContext reportServiceContext)
        {
            var result = new List<CollectionTypeDetails>
            {
                GetCollectionType(reportServiceContext.CollectionYear, reportServiceContext.CollectionReturnCodeNCS, nameof(reportServiceContext.CollectionReturnCodeNCS)),
                GetCollectionType(reportServiceContext.CollectionYear, reportServiceContext.CollectionReturnCodeDC, nameof(reportServiceContext.CollectionReturnCodeDC)),
                GetCollectionType(reportServiceContext.CollectionYear, reportServiceContext.CollectionReturnCodeApp, nameof(reportServiceContext.CollectionReturnCodeApp)),
                GetCollectionType(reportServiceContext.CollectionYear, reportServiceContext.CollectionReturnCodeESF, nameof(reportServiceContext.CollectionReturnCodeESF)),
            };

            return result.Where(r => r != null).ToList();
        }

        public CollectionTypeDetails GetCollectionType(int collectionYear, string value, string collectionType)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            var collectionTypeFormatter = _collectionTypeFormatters.First(ctf => ctf.CollectionReturnCode.Equals(collectionType, StringComparison.OrdinalIgnoreCase));

            if (collectionTypeFormatter == null)
            {
                throw new ApplicationException($"Unexpected Collection Type {collectionType}");
            }

            return collectionTypeFormatter.SetupType(collectionYear, value);
        }

    }
}
