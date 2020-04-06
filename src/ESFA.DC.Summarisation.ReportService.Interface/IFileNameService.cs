namespace ESFA.DC.Summarisation.ReportService.Interface
{
    public interface IFileNameService
    {
        string Generate(string reportBaseName, string period);
    }
}
