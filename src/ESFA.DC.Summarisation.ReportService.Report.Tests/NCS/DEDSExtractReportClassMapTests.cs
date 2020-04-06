using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using ESFA.DC.Summarisation.ReportService.Model;
using ESFA.DC.Summarisation.ReportService.Report.NCS;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.Summarisation.ReportService.Service.Tests.NCS
{
    public class DEDSExtractReportClassMapTests
    {
        [Fact]
        public void Map_Columns()
        {
            var orderedColumns = new string[]
            {
                "Id",
               "CollectionReturnCode",
               "ukprn",
               "OrganisationId",
               "PeriodTypeCode",
               "Period",
               "FundingStreamPeriodCode",
               "CollectionType",
               "ContractAllocationNumber",
               "UoPCode",
               "DeliverableCode",
               "ActualVolume",
               "ActualValue"
            };

            var input = new List<NcsDed>();

            using (var stream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(stream, Encoding.UTF8, 8096, true))
                {
                    using (var csvWriter = new CsvWriter(streamWriter))
                    {
                        csvWriter.Configuration.RegisterClassMap<DEDSExtractReportClassMap>();

                        csvWriter.WriteRecords(input);
                    }
                }

                stream.Position = 0;

                using (var streamReader = new StreamReader(stream))
                {
                    using (var csvReader = new CsvReader(streamReader))
                    {
                        csvReader.Read();
                        csvReader.ReadHeader();
                        var header = csvReader.Context.HeaderRecord;

                        header.Should().ContainInOrder(orderedColumns);

                        header.Should().HaveCount(13);
                    }
                }
            }
        }

    }
}
