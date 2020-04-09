using Autofac;
using ESFA.DC.FileService.Config;
using ESFA.DC.ServiceFabric.Common.Config;
using ESFA.DC.ServiceFabric.Common.Config.Interface;
using ESFA.DC.Summarisation.ReportService.Modules;
using ESFA.DC.Summarisation.ReportService.Stateless.Config;
using ESFA.DC.Summarisation.ReportService.Stateless.Modules;

namespace ESFA.DC.Summarisation.ReportService.Stateless
{
    public class DIComposition
    {
        public static ContainerBuilder BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();

            IServiceFabricConfigurationService serviceFabricConfigurationService = new ServiceFabricConfigurationService();

            var statelessServiceConfiguration = serviceFabricConfigurationService.GetConfigSectionAsStatelessServiceConfiguration();
            var reportServiceConfiguration = serviceFabricConfigurationService.GetConfigSectionAs<ReportServiceConfiguration>("ReportServiceConfiguration");

            var azureStorageFileServiceConfiguration = serviceFabricConfigurationService.GetConfigSectionAs<AzureStorageFileServiceConfiguration>("AzureStorageFileServiceConfiguration");
            var ioConfiguration = serviceFabricConfigurationService.GetConfigSectionAs<IOConfiguration>("IOConfiguration");

            containerBuilder.RegisterModule(new StatelessBaseModule(statelessServiceConfiguration));
            containerBuilder.RegisterModule(new IOModule(azureStorageFileServiceConfiguration, ioConfiguration));
            containerBuilder.RegisterModule(new DataModule(reportServiceConfiguration));
            containerBuilder.RegisterModule<ReportsServiceModule>();
            containerBuilder.RegisterModule<ReportsModule>();

            return containerBuilder;
        }
    }
}
