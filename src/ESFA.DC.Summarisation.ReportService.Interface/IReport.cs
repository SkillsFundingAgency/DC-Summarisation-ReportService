using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.Summarisation.ReportService.Interface
{
    public interface IReport
    {
        string TaskName { get; }

        Task<IEnumerable<string>> GenerateAsync(IReportServiceContext reportServiceContext, CancellationToken cancellationToken);

    }
}
