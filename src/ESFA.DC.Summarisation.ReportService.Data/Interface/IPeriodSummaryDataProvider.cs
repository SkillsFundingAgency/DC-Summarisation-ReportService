﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.Summarisation.ReportService.Interface.Model;
using ESFA.DC.Summarisation.ReportService.Model;

namespace ESFA.DC.Summarisation.ReportService.Data.Interface
{
    public interface IPeriodSummaryDataProvider
    {
        Task<IEnumerable<PeriodSummary>> ProvideAsync(string period, string collectionType, CancellationToken cancellationToken);
        Task<IEnumerable<PeriodSummary>> ProvideAsync(List<CollectionTypeDetails> collectionTypes, CancellationToken cancellationToken);
    }
}
