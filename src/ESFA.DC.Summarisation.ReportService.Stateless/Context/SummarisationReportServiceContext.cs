using System.Collections.Generic;
using System.Linq;
using ESFA.DC.JobContextManager.Model;
using ESFA.DC.Summarisation.ReportService.Interface;
using ESFA.DC.Summarisation.ReportService.Stateless.Constants;

namespace ESFA.DC.Summarisation.ReportService.Stateless.Context
{
    public class SummarisationReportServiceContext : IReportServiceContext
    {
        private readonly JobContextMessage _jobContextMessage;

        public SummarisationReportServiceContext(JobContextMessage jobContextMessage)
        {
            _jobContextMessage = jobContextMessage;
        }

        public string Container => _jobContextMessage.KeyValuePairs[ContextConstants.Container].ToString();
        public IEnumerable<string> Tasks => _jobContextMessage.Topics[_jobContextMessage.TopicPointer].Tasks.SelectMany(x => x.Tasks);

        public int CollectionYear => int.Parse(_jobContextMessage.KeyValuePairs[ContextConstants.CollectionYear].ToString());
        public int ReturnPeriod => int.Parse(_jobContextMessage.KeyValuePairs[ContextConstants.ReturnPeriod].ToString());
    }
}