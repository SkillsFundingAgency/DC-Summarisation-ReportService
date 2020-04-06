using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.CsvService.Interface;
using ESFA.DC.Logging.Interfaces;
using ESFA.DC.Summarisation.ReportService.Data.Interface;
using ESFA.DC.Summarisation.ReportService.Interface;
using ESFA.DC.Summarisation.ReportService.Report.Constants;

namespace ESFA.DC.Summarisation.ReportService.Report.PeriodSummary
{
    public class PeriodSummaryReport : IReport
    {
        private readonly IFileNameService _fileNameService;
        private readonly ICsvFileService _csvService;
        private readonly IPeriodSummaryDataProvider _ncsDedsExtractDataProvider;
        private readonly ILogger _logger;

        public PeriodSummaryReport(
            IFileNameService fileNameService,
            ICsvFileService csvService,
            IPeriodSummaryDataProvider ncsDedsExtractDataProvider,
            ILogger logger
            )
        {
            _fileNameService = fileNameService;
            _csvService = csvService;
            _ncsDedsExtractDataProvider = ncsDedsExtractDataProvider;
            _logger = logger;
        }

        public string TaskName => ReportTaskNameConstants.PeriodSummaryReport;

        public async Task<IEnumerable<string>> GenerateAsync(IReportServiceContext reportServiceContext, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Report Generation for Period Summary started");

            var reportBaseName = string.Empty;
            var periodPrefix = string.Empty;
            var period = string.Empty;
            var collectionType = string.Empty;

            switch (reportServiceContext.TaskType)
            {
                case ReportTaskNameConstants.PeriodSummaryTaskType:
                    reportBaseName = "NCS Summarised Actuals Data Extract";
                    periodPrefix = "N";
                    period = $"N{reportServiceContext.ReturnPeriod:D2}";
                    collectionType = $"NCS{reportServiceContext.CollectionYear:D4}";
                    break;
                default:
                    throw new ArgumentException($"Unexpected TaskType {reportServiceContext.TaskType}", nameof(reportServiceContext));
            }

            var fileName = _fileNameService.Generate(reportBaseName, period);

            var periods = await _ncsDedsExtractDataProvider.ProvideAsync(period, collectionType, cancellationToken);

            await _csvService.WriteAsync<Model.PeriodSummary, PeriodSummaryReportClassMap>(periods, fileName, reportServiceContext.Container, cancellationToken);

            _logger.LogInfo("Report Generation for Period Summary finished");
            return new[] { fileName };
        }
    }
}
