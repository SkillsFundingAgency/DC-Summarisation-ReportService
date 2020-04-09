using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.Logging.Interfaces;
using ESFA.DC.Summarisation.ReportService.Interface;

namespace ESFA.DC.Summarisation.ReportService.Service
{
    public class ReportGenerationService : IReportGenerationService
    {
        private readonly ILogger _logger;
        private readonly IFileNameService _fileNameService;
        private readonly IEnumerable<IReport> _reports;

        public ReportGenerationService(
            ILogger logger,
            IFileNameService fileNameService,
            IEnumerable<IReport> reports)
        {
            _logger = logger;
            _fileNameService = fileNameService;
            _reports = reports;
        }

        public async Task<IEnumerable<string>> GenerateAsync(IReportServiceContext reportServiceContext, CancellationToken cancellationToken)
        {
            var reportOutputFilenames = new List<string>();

            _logger.LogDebug("Inside Summarisation ReportService callback");

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                // list of reports to be generated
                var reportsToBeGenerated = _reports.Where(x => reportServiceContext.Tasks.Contains(x.TaskName)).ToList();

                if (!_reports.Any())
                {
                    _logger.LogDebug($"No reports found.");
                }

                foreach (var report in reportsToBeGenerated)
                {
                    _logger.LogInfo($"Starting {report.GetType().Name}");

                    var reportsGenerated = await report.GenerateAsync(reportServiceContext, cancellationToken);
                    reportOutputFilenames.AddRange(reportsGenerated);

                    _logger.LogInfo($"Finishing {report.GetType().Name}");
                }

                _logger.LogDebug("Completed Summarisation ReportService GenerateAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Summarisation ReportService callback exception {ex.Message}", ex);
                throw;
            }

            return reportOutputFilenames;
        }

    }
}