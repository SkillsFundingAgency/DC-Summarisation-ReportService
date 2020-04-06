using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.Summarisation.ReportService.Data.Model;

namespace ESFA.DC.Summarisation.ReportService.Data.Interface
{
    public interface IFcsRepositoryService
    {
        Task<IEnumerable<Organisation>> RetrieveOrganisationsAsync(string[] OrganisationIds, CancellationToken cancellationToken);
    }
}
