using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.Summarisation.ReportService.Data.Interface;
using ESFA.DC.Summarisation.ReportService.Data.Model;

namespace ESFA.DC.Summarisation.ReportService.Data.Repository
{
    public class FcsRepositoryService : AbstractRepositoryService, IFcsRepositoryService
    {
        private readonly string _connectionString;

        public FcsRepositoryService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Organisation>> RetrieveOrganisationsAsync(string[] OrganisationIds, CancellationToken cancellationToken)
        {
            var sqlQuery = @"SELECT 
                                MAX(ContractVersionNumber) ContractVersionNumber, OrganisationIdentifier, UKPRN
                            FROM [dbo].[Contract] c
                            JOIN [dbo].[Contractor] cr on c.ContractorId = cr.Id
                            WHERE cr.OrganisationIdentifier in (@OrganisationIds)
                            GROUP BY OrganisationIdentifier, ukprn";

            return await ExecuteSqlWithParametersAsync<Organisation>(_connectionString, new { OrganisationIds } , sqlQuery, cancellationToken);
        }
    }
}
