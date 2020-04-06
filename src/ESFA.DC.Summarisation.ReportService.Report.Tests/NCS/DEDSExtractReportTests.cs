using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using ESFA.DC.Summarisation.ReportService.Report.NCS;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.Summarisation.ReportService.Service.Tests.NCS
{
    public class DEDSExtractReportTests
    {
        [Fact(Skip = "To be used for local end to end test of report. Replace connection strings as appropriate.")]
        public async Task GenerateAsync()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            var contextMock = new Mock<IReportServiceContext>();
            contextMock.SetupGet(c => c.Period).Returns("N01");
            contextMock.SetupGet(c => c.CollectionType).Returns("NCS1920");
            contextMock.SetupGet(c => c.Container).Returns(@"C:\Temp");

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(sm => sm.GetNowUtc()).Returns(DateTime.Now);

            IFileNameService fileNameService = new FileNameService(dateTimeProvider.Object);

            IFileService fileService = new FileSystemFileService();
            ICsvFileService csvService = new CsvFileService(fileService);
            var loggerMock = new Mock<ILogger>();

            IFcsRepositoryService fcsRepositoryService = new FcsRepositoryService("(local);Database=FCS;User Id=User;Password=Password1;Encrypt=True;");
            ISummarisedActualsRepositoryService summarisedActualsRepositoryService = new SummarisedActualsRepositoryService("data source=(local);initial catalog=SummarisedActuals;User Id=User;Password=Password1;Encrypt=True;");

            INcsDedsExtractDataProvider ncsDedsExtractDataProvider = new NcsDedsExtractDataProvider(summarisedActualsRepositoryService, fcsRepositoryService, loggerMock.Object);

            var dedsExtractReport = new DEDSExtractReport(fileNameService, csvService, ncsDedsExtractDataProvider, loggerMock.Object);

            // Act
            var result = await dedsExtractReport.GenerateAsync(contextMock.Object, cancellationToken);

            // Assert
            result.Should().NotBeNull();

        }
    }
}
