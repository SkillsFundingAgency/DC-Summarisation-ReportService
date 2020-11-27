using ESFA.DC.Summarisation.ReportService.Interface;

namespace ESFA.DC.Summarisation.ReportService.Stateless.Config
{
    public class ReportServiceConfiguration : IReportServiceConfiguration
    {
        public string SummarisedActualsConnectionString { get; set;  }
        public string FcsConnectionString { get; set; }
    }
}
