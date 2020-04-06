using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.Summarisation.ReportService.Interface;
using ESFA.DC.Summarisation.ReportService.Model;

namespace ESFA.DC.Summarisation.ReportService.Data.Interface
{
    public interface INcsDedsExtractDataProvider
    {
        Task<IEnumerable<NcsDed>> ProvideAsync(IReportServiceContext reportServiceContext, CancellationToken cancellationToken);
    }
}
