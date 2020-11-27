using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.Summarisation.ReportService.Interface;
using ESFA.DC.Summarisation.ReportService.Report.PeriodSummary;
using ESFA.DC.Summarisation.ReportService.Service.CollectionTypeFormatters;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.Summarisation.ReportService.Service.Tests.PeriodSummary
{
    public class PeriodSummaryCollectionTypeTests
    {
        [Fact]
        public void NoCollectionTypesReturnsEmptyList()
        {
            // Arrange
            var ReportServiceContextMock = new Mock<IReportServiceContext>();
            ReportServiceContextMock.Setup(rsc => rsc.CollectionReturnCodeNCS).Returns(string.Empty);
            ReportServiceContextMock.Setup(rsc => rsc.CollectionReturnCodeApp).Returns(string.Empty);
            ReportServiceContextMock.Setup(rsc => rsc.CollectionReturnCodeESF).Returns(string.Empty);
            ReportServiceContextMock.Setup(rsc => rsc.CollectionReturnCodeDC).Returns(string.Empty);

            var periodSummaryReport = new PeriodSummaryReport(null, null, GetCollectionTypeFormatters(), null, null);

            // Act
            var result = periodSummaryReport.GetCollectionTypes(ReportServiceContextMock.Object);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Theory]
        [InlineData("N01", "", "", "", "NCS2021", "N01", "N01/NCS Summarised Actuals Data Extract N01 20200102-030405.csv")]
        [InlineData("", "APPS38", "", "", "APPS", "APPS38", null)]
        [InlineData("", "", "ESF54", "", "ESF", "ESF54", null)]
        [InlineData("", "", "", "R11", "ILR2021", "R11", "R11/Data Extract Report R11 20200102-030405.csv")]
        public void OneCollectionSetReturnsOneFormatter(string ncs, string app, string esf, string dc, string collectionType, string collectionReturn, string fileName)
        {
            // Arrange
            var ReportServiceContextMock = new Mock<IReportServiceContext>();
            ReportServiceContextMock.Setup(rsc => rsc.CollectionYear).Returns(2021);
            ReportServiceContextMock.Setup(rsc => rsc.CollectionReturnCodeNCS).Returns(ncs);
            ReportServiceContextMock.Setup(rsc => rsc.CollectionReturnCodeApp).Returns(app);
            ReportServiceContextMock.Setup(rsc => rsc.CollectionReturnCodeESF).Returns(esf);
            ReportServiceContextMock.Setup(rsc => rsc.CollectionReturnCodeDC).Returns(dc);

            var periodSummaryReport = new PeriodSummaryReport(null, null, GetCollectionTypeFormatters(), null, null);

            // Act
            var result = periodSummaryReport.GetCollectionTypes(ReportServiceContextMock.Object);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.Single().CollectionType.Should().Be(collectionType);
            result.Single().CollectionReturn.Should().Be(collectionReturn);
            result.Single().FileName.Should().Be(fileName);
        }

        private static IEnumerable<ICollectionTypeFormatter> GetCollectionTypeFormatters()
        {
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(dtp => dtp.GetNowUtc()).Returns(new DateTime(2020, 1, 2, 3, 4, 5));

            var fileNameService = new FileNameService(dateTimeProviderMock.Object);

            return new List<ICollectionTypeFormatter>
            {
                new NCSCollectionTypeFormatter(fileNameService),
                new APPSCollectionTypeFormatter(),
                new ESFCollectionTypeFormatter(),
                new ILRCollectionTypeFormatter(fileNameService)
            };
        }
    }
}