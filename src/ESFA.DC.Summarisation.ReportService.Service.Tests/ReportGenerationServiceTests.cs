using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.Logging.Interfaces;
using ESFA.DC.Summarisation.ReportService.Interface;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.Summarisation.ReportService.Service.Tests
{
    public class ReportGenerationServiceTests
    {
        [Fact]
        public async Task GenerateAsync()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var containerName = "Container";
            IEnumerable<string> taskList = new List<string>
            {
                "Task1"
            };

            var loggerMock = new Mock<ILogger>();
            var fileNameServiceMock = new Mock<IFileNameService>();
            var contextMock = new Mock<IReportServiceContext>();

            contextMock.Setup(c => c.Container).Returns(containerName);
            contextMock.Setup(c => c.Tasks).Returns(taskList);

            var reportMock = new Mock<IReport>();

            reportMock.Setup(r => r.TaskName).Returns("Task1");
            reportMock.Setup(r => r.GenerateAsync(contextMock.Object, cancellationToken)).Returns(Task.FromResult(taskList));

            IEnumerable<IReport> reportsMock = new List<IReport>
            {
                reportMock.Object
            };

            var reportService = new ReportGenerationService(loggerMock.Object, fileNameServiceMock.Object, reportsMock);

            // Act
            var result = await reportService.GenerateAsync(contextMock.Object, cancellationToken);

            // Assert
            result.Should().BeEquivalentTo(taskList);

            loggerMock.VerifyAll();
            fileNameServiceMock.VerifyAll();
        }
    }
}