namespace ESFA.DC.Summarisation.ReportService.Model
{
    public class PeriodSummary
    {
        public int Id { get; set; }
        public string CollectionReturnCode { get; set; }
        public int UKPRN { get; set; }
        public string OrganisationId { get; set; }
        public string PeriodTypeCode { get; set; }
        public int Period { get; set; }
        public string FundingStreamPeriodCode { get; set; }
        public string CollectionType { get; set; }
        public string ContractAllocationNumber { get; set; }
        public string UoPCode { get; set; }
        public int DeliverableCode { get; set; }
        public int ActualVolume { get; set; }
        public decimal ActualValue { get; set; }
    }
}
