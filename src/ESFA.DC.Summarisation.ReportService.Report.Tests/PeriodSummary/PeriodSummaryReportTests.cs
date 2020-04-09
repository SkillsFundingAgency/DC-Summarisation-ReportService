using System;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.CsvService;
using ESFA.DC.CsvService.Interface;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.FileService;
using ESFA.DC.FileService.Interface;
using ESFA.DC.Logging.Interfaces;
using ESFA.DC.Summarisation.ReportService.Data;
using ESFA.DC.Summarisation.ReportService.Data.Interface;
using ESFA.DC.Summarisation.ReportService.Data.Repository;
using ESFA.DC.Summarisation.ReportService.Interface;
using ESFA.DC.Summarisation.ReportService.Report.PeriodSummary;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.Summarisation.ReportService.Service.Tests.PeriodSummary
{
    public class PeriodSummaryReportTests
    {
        [Fact(Skip = "To be used for local end to end test of report. Replace connection strings as appropriate.")]
        public async Task GenerateAsync()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            var contextMock = new Mock<IReportServiceContext>();
            contextMock.SetupGet(c => c.Container).Returns(@"C:\Temp");
            contextMock.SetupGet(c => c.CollectionYear).Returns(1920);
            contextMock.SetupGet(c => c.ReturnPeriod).Returns(1);

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(sm => sm.GetNowUtc()).Returns(DateTime.Now);

            IFileNameService fileNameService = new FileNameService(dateTimeProvider.Object);

            IFileService fileService = new FileSystemFileService();
            ICsvFileService csvService = new CsvFileService(fileService);
            var loggerMock = new Mock<ILogger>();

            IFcsRepositoryService fcsRepositoryService = new FcsRepositoryService("(local);Database=FCS;User Id=User;Password=Password1;Encrypt=True;");
            ISummarisedActualsRepositoryService summarisedActualsRepositoryService = new SummarisedActualsRepositoryService("data source=(local);initial catalog=SummarisedActuals;User Id=User;Password=Password1;Encrypt=True;");

            IPeriodSummaryDataProvider periodSummaryDataProvider = new PeriodSummaryDataProvider(summarisedActualsRepositoryService, fcsRepositoryService, loggerMock.Object);

            var periodSummaryReport = new PeriodSummaryReport(fileNameService, csvService, periodSummaryDataProvider, loggerMock.Object);

            // Act
            var result = await periodSummaryReport.GenerateAsync(contextMock.Object, cancellationToken);

            // Assert
            result.Should().NotBeNull();

        }
    }
}
