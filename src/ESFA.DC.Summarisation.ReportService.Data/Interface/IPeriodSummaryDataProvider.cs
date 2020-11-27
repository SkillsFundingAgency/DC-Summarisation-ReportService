using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.Summarisation.ReportService.Interface.Model;
using ESFA.DC.Summarisation.ReportService.Model;

namespace ESFA.DC.Summarisation.ReportService.Data.Interface
{
    public interface IPeriodSummaryDataProvider
    {
        Task<IEnumerable<PeriodSummary>> ProvideAsync(ICollection<CollectionTypeDetails> collectionTypes, CancellationToken cancellationToken);
    }
}
