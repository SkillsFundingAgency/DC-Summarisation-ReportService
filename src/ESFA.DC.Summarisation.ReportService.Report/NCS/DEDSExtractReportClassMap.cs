using CsvHelper.Configuration;
using ESFA.DC.Summarisation.ReportService.Model;

namespace ESFA.DC.Summarisation.ReportService.Report.NCS
{
    public class DEDSExtractReportClassMap : ClassMap<NcsDed>
    {
        public DEDSExtractReportClassMap()
        {
            var index = 0;

            Map(m => m.Id).Name(@"Id").Index(++index);
            Map(m => m.CollectionReturnCode).Name(@"CollectionReturnCode").Index(++index);
            Map(m => m.UKPRN).Name(@"ukprn").Index(++index);
            Map(m => m.OrganisationId).Name(@"OrganisationId").Index(++index);
            Map(m => m.PeriodTypeCode).Name(@"PeriodTypeCode").Index(++index);
            Map(m => m.Period).Name(@"Period").Index(++index);
            Map(m => m.FundingStreamPeriodCode).Name(@"FundingStreamPeriodCode").Index(++index);
            Map(m => m.CollectionType).Name(@"CollectionType").Index(++index);
            Map(m => m.ContractAllocationNumber).Name(@"ContractAllocationNumber").Index(++index);
            Map(m => m.UoPCode).Name(@"UoPCode").Index(++index);
            Map(m => m.DeliverableCode).Name(@"DeliverableCode").Index(++index);
            Map(m => m.ActualVolume).Name(@"ActualVolume").Index(++index);
            Map(m => m.ActualValue).Name(@"ActualValue").Index(++index);
        }
    }
}