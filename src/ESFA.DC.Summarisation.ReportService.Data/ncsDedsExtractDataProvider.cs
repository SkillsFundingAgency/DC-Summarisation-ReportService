using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.Logging.Interfaces;
using ESFA.DC.Summarisation.ReportService.Data.Interface;
using ESFA.DC.Summarisation.ReportService.Data.Model;
using ESFA.DC.Summarisation.ReportService.Interface;
using ESFA.DC.Summarisation.ReportService.Model;

namespace ESFA.DC.Summarisation.ReportService.Data
{
    public class NcsDedsExtractDataProvider : INcsDedsExtractDataProvider
    {
        private readonly ISummarisedActualsRepositoryService _summarisedActualsRepositoryService;
        private readonly IFcsRepositoryService _fcsRepositoryService;
        private readonly ILogger _logger;

        public NcsDedsExtractDataProvider(
            ISummarisedActualsRepositoryService summarisedActualsRepositoryService,
            IFcsRepositoryService fcsRepositoryService,
            ILogger logger)
        {
            _summarisedActualsRepositoryService = summarisedActualsRepositoryService;
            _fcsRepositoryService = fcsRepositoryService;
            _logger = logger;
        }


        public async Task<IEnumerable<NcsDed>> ProvideAsync(IReportServiceContext reportServiceContext, CancellationToken cancellationToken)
        {
            var summarisedActuals = await _summarisedActualsRepositoryService.RetrieveSummarisedActualsAsync(reportServiceContext.Period, reportServiceContext.CollectionType, cancellationToken);

            var distinctOrgIds = summarisedActuals.Select((sa => sa.OrganisationId)).Distinct().ToList();
            var organisations = await _fcsRepositoryService.RetrieveOrganisationsAsync(distinctOrgIds.ToArray(), cancellationToken);

            IEnumerable<NcsDed> ncdDeds = CombineActualsAndOrganisations(summarisedActuals, organisations);

            return ncdDeds;
        }

        private IEnumerable<NcsDed> CombineActualsAndOrganisations(IEnumerable<SummarisedActual> summarisedActuals, IEnumerable<Organisation> organisations)
        {
            var results = new List<NcsDed>(summarisedActuals?.Count() ?? 0);

            if (summarisedActuals == null)
            {
                return results;
            }

            foreach (var summarisedActual in summarisedActuals)
            {
                var ncdDed = new NcsDed
                {
                    Id = summarisedActual.ID,
                    CollectionReturnCode = summarisedActual.CollectionReturnCode,
                    UKPRN = organisations?.FirstOrDefault(o => o.OrganisationIdentifier == summarisedActual.OrganisationId)?.UKPRN ?? 0,
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
