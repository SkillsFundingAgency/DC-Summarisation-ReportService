using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using ESFA.DC.JobContextManager.Model;
using ESFA.DC.Summarisation.ReportService.Constants;
using ESFA.DC.Summarisation.ReportService.Interface;

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

        public string CollectionReturnCodeDC => GetValueIfDefined(ContextConstants.CollectionReturnCodeDC);
        public string CollectionReturnCodeESF => GetValueIfDefined(ContextConstants.CollectionReturnCodeESF);
        public string CollectionReturnCodeApp => GetValueIfDefined(ContextConstants.CollectionReturnCodeApp);
        public string CollectionReturnCodeNCS => GetValueIfDefined(ContextConstants.CollectionReturnCodeNCS);

        private string GetValueIfDefined(string key)
        {
            return _jobContextMessage.KeyValuePairs.TryGetValue(key, out object collectionReturnCodeDC) 
                ? collectionReturnCodeDC.ToString() 
                : null;
        }
    }
}