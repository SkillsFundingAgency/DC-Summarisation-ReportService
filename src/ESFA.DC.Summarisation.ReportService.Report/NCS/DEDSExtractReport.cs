using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.CsvService.Interface;
using ESFA.DC.Logging.Interfaces;
using ESFA.DC.Summarisation.ReportService.Data.Interface;
using ESFA.DC.Summarisation.ReportService.Interface;
using ESFA.DC.Summarisation.ReportService.Model;
using ESFA.DC.Summarisation.ReportService.Report.Constants;

namespace ESFA.DC.Summarisation.ReportService.Report.NCS
{
    public class DEDSExtractReport : IReport
    {
        private readonly IFileNameService _fileNameService;
        private readonly ICsvFileService _csvService;
        private readonly INcsDedsExtractDataProvider _ncsDedsExtractDataProvider;
        private readonly ILogger _logger;

        public DEDSExtractReport(
            IFileNameService fileNameService,
            ICsvFileService csvService,
            INcsDedsExtractDataProvider ncsDedsExtractDataProvider,
            ILogger logger
            )
        {
            _fileNameService = fileNameService;
            _csvService = csvService;
            _ncsDedsExtractDataProvider = ncsDedsExtractDataProvider;
            _logger = logger;
        }

        public string TaskName => ReportTaskNameConstants.DEDSExtractReport;

        public async Task<IEnumerable<string>> GenerateAsync(IReportServiceContext reportServiceContext, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Report Generation for NCS DEDS Extract started");
            var fileName = _fileNameService.Generate(reportServiceContext, "NCS Summarised Actuals Data Extract");

            var deds = await _ncsDedsExtractDataProvider.ProvideAsync(reportServiceContext, cancellationToken);

            await _csvService.WriteAsync<NcsDed, DEDSExtractReportClassMap>(deds, fileName, reportServiceContext.Container, cancellationToken);

            _logger.LogInfo("Report Generation for NCS DEDS Extract finished");
            return new[] { fileName };
        }
    }
}
