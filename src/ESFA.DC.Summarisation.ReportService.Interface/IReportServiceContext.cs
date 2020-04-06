namespace ESFA.DC.Summarisation.ReportService.Interface
{
    public interface IReportServiceContext
    {
        string Container { get; }
        int CollectionYear { get; }
        int ReturnPeriod { get; }
        string TaskType { get; }
    }
}
