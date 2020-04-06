using System;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.Summarisation.ReportService.Interface;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.Summarisation.ReportService.Service.Tests
{
    public class FileNameServiceTests
    {
        [Fact]
        public void Generate()
        {
            // Arrange
            var contextMock = new Mock<IReportServiceContext>();
            contextMock.SetupGet(c => c.Period).Returns("N01");

            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(p => p.GetNowUtc()).Returns(new DateTime(2020, 02, 03, 8, 9, 10));

            var service = new FileNameService(dateTimeProviderMock.Object);

            // Act
            var result = service.Generate(contextMock.Object, "Base Name");

            // Assert
            result.Should().Be("Base Name N01 20200203-080910");
        }
    }
}
