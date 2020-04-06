namespace ESFA.DC.Summarisation.ReportService.Interface
{
    public interface IFileNameService
    {
        string Generate(IReportServiceContext reportServiceContext, string baseFileName);
    }
}
