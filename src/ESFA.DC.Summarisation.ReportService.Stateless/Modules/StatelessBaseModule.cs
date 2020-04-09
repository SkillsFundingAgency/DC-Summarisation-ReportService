using Autofac;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.FileService;
using ESFA.DC.FileService.Interface;
using ESFA.DC.JobContextManager.Interface;
using ESFA.DC.JobContextManager.Model;
using ESFA.DC.ServiceFabric.Common.Config.Interface;
using ESFA.DC.ServiceFabric.Common.Modules;

namespace ESFA.DC.Summarisation.ReportService.Stateless.Modules
{
    public class StatelessBaseModule : Module
    {
        private readonly IStatelessServiceConfiguration _statelessServiceConfiguration;

        public StatelessBaseModule(IStatelessServiceConfiguration statelessServiceConfiguration)
        {
            _statelessServiceConfiguration = statelessServiceConfiguration;
        }

        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new StatelessServiceModule(_statelessServiceConfiguration));
            containerBuilder.RegisterModule<SerializationModule>();
            containerBuilder.RegisterType<DateTimeProvider.DateTimeProvider>().As<IDateTimeProvider>();
            containerBuilder.RegisterType<MessageHandler>().As<IMessageHandler<JobContextMessage>>();
            containerBuilder.RegisterType<ZipArchiveService>().As<IZipArchiveService>();
        }
    }
}