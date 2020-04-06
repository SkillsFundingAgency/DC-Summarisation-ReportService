using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.Summarisation.ReportService.Data.Interface;
using ESFA.DC.Summarisation.ReportService.Data.Model;

namespace ESFA.DC.Summarisation.ReportService.Data.Repository
{
    public class SummarisedActualsRepositoryService : AbstractRepositoryService, ISummarisedActualsRepositoryService
    {
        private readonly string _connectionString;

        public SummarisedActualsRepositoryService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<SummarisedActual>> RetrieveSummarisedActualsAsync(string collectionReturnCode, string collectionType, CancellationToken cancellationToken)
        {
            var sqlQuery = @"SELECT
                                sa.ID, CollectionReturnCode, OrganisationId, PeriodTypeCode,
                                [Period], FundingStreamPeriodCode, CollectionType, ContractAllocationNumber,
                                UoPCode, DeliverableCode, ActualVolume, ActualValue
                              FROM [dbo].[SummarisedActuals] sa
                              JOIN [dbo].[CollectionReturn] cr on cr.Id = sa.CollectionReturnId
                              WHERE 
                              cr.CollectionReturnCode = @collectionReturnCode and
                              cr.CollectionType = @collectionType";

            return await ExecuteSqlWithParametersAsync<SummarisedActual>(_connectionString, new { collectionReturnCode, collectionType } , sqlQuery, cancellationToken);
        }
    }
}
