namespace ESFA.DC.Summarisation.ReportService.Interface
{
    public interface IReportServiceConfiguration
    {
        string SummarisedActualsConnectionString { get; }

        string FcsConnectionString { get; }
    }
}