using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.Logging.Interfaces;
using ESFA.DC.Summarisation.ReportService.Data.Interface;
using ESFA.DC.Summarisation.ReportService.Data.Model;
using ESFA.DC.Summarisation.ReportService.Interface.Model;
using ESFA.DC.Summarisation.ReportService.Model;

namespace ESFA.DC.Summarisation.ReportService.Data
{
    public class PeriodSummaryDataProvider : IPeriodSummaryDataProvider
    {
        private readonly ISummarisedActualsRepositoryService _summarisedActualsRepositoryService;
        private readonly IFcsRepositoryService _fcsRepositoryService;
        private readonly ILogger _logger;

        public PeriodSummaryDataProvider(
            ISummarisedActualsRepositoryService summarisedActualsRepositoryService,
            IFcsRepositoryService fcsRepositoryService,
            ILogger logger)
        {
            _summarisedActualsRepositoryService = summarisedActualsRepositoryService;
            _fcsRepositoryService = fcsRepositoryService;
            _logger = logger;
        }


        public async Task<IEnumerable<PeriodSummary>> ProvideAsync(string period, string collectionType, CancellationToken cancellationToken)
        {
            var summarisedActuals = await _summarisedActualsRepositoryService.RetrieveSummarisedActualsAsync(period, collectionType, cancellationToken);

            var distinctOrgIds = summarisedActuals.Select(sa => sa.OrganisationId).Distinct().ToList();
            var organisations = await _fcsRepositoryService.RetrieveOrganisationsAsync(distinctOrgIds.ToArray(), cancellationToken);

            IEnumerable<PeriodSummary> PeriodSummaries = CombineActualsAndOrganisations(summarisedActuals, organisations);

            return PeriodSummaries;
        }

        public async Task<IEnumerable<PeriodSummary>> ProvideAsync(
            List<CollectionTypeDetails> collectionTypes,
            CancellationToken cancellationToken)
        {
            var summarisedActuals = new List<SummarisedActual>();

            foreach (var collectionType in collectionTypes)
            {
                summarisedActuals.AddRange(
                    await _summarisedActualsRepositoryService.RetrieveSummarisedActualsAsync(
                        collectionType.CollectionReturn, 
                        collectionType.CollectionType, 
                        cancellationToken));
            }

            var distinctOrgIds = summarisedActuals.Select(sa => sa.OrganisationId).Distinct().ToList();
            var organisations = await _fcsRepositoryService.RetrieveOrganisationsAsync(distinctOrgIds.ToArray(), cancellationToken);

            IEnumerable<PeriodSummary> PeriodSummaries = CombineActualsAndOrganisations(summarisedActuals, organisations);

            return PeriodSummaries;
        }


        private IEnumerable<PeriodSummary> CombineActualsAndOrganisations(IEnumerable<SummarisedActual> summarisedActuals, IEnumerable<Organisation> organisations)
        {
            var results = new List<PeriodSummary>(summarisedActuals?.Count() ?? 0);

            if (summarisedActuals == null)
            {
                return results;
            }

            var orgLookup = organisations?.ToDictionary(o => o.OrganisationIdentifier, o => o.UKPRN) ?? new Dictionary<string, int>();

            foreach (var summarisedActual in summarisedActuals)
            {
                orgLookup.TryGetValue(summarisedActual.OrganisationId, out var ukprn);

                var ncdDed = new PeriodSummary
                {
                    Id = summarisedActual.ID,
                    CollectionReturnCode = summarisedActual.CollectionReturnCode,
                    UKPRN = ukprn,
                    OrganisationId = summarisedActual.OrganisationId,
                    PeriodTypeCode = summarisedActual.PeriodTypeCode,
                    Period = summarisedActual.Period,
                    FundingStreamPeriodCode = summarisedActual.FundingStreamPeriodCode,
                    CollectionType = summarisedActual.CollectionType,
                    ContractAllocationNumber = summarisedActual.ContractAllocationNumber,
                    UoPCode = summarisedActual.UoPCode,
                    DeliverableCode = summarisedActual.DeliverableCode,
                    ActualVolume = summarisedActual.ActualVolume,
                    ActualValue = summarisedActual.ActualValue
                };

                results.Add(ncdDed);
            }

            return results;
        }
    }
}
