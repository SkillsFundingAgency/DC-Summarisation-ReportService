namespace ESFA.DC.Summarisation.ReportService.Interface
{
    public interface IReportServiceContext
    {
        string Period { get; }
        string CollectionType { get; }
        string Container { get; }
    }
}
