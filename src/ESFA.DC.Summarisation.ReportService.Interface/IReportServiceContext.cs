using System.Collections.Generic;

namespace ESFA.DC.Summarisation.ReportService.Interface
{
    public interface IReportServiceContext
    {
        string Container { get; }
        IEnumerable<string> Tasks { get; }

        int CollectionYear { get; }
        int ReturnPeriod { get; }
    }
}
