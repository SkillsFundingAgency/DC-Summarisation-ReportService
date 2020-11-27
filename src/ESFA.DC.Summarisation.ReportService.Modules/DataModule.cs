using Autofac;
using ESFA.DC.Summarisation.ReportService.Data.Interface;
using ESFA.DC.Summarisation.ReportService.Data.Repository;
using ESFA.DC.Summarisation.ReportService.Interface;

namespace ESFA.DC.Summarisation.ReportService.Modules
{
    public class DataModule : Module
    {
        private readonly string ConnectionString = "connectionString";
        private readonly IReportServiceConfiguration _reportServiceConfiguration;

        public DataModule(IReportServiceConfiguration reportServiceConfiguration)
        {
            _reportServiceConfiguration = reportServiceConfiguration;
        }

        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<FcsRepositoryService>().As<IFcsRepositoryService>().WithParameter(ConnectionString, _reportServiceConfiguration.FcsConnectionString);
            containerBuilder.RegisterType<SummarisedActualsRepositoryService>().As<ISummarisedActualsRepositoryService>().WithParameter(ConnectionString, _reportServiceConfiguration.SummarisedActualsConnectionString);
        }
    }
}