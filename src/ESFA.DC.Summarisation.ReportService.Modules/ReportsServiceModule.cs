using Autofac;
using ESFA.DC.CsvService;
using ESFA.DC.CsvService.Interface;
using ESFA.DC.Summarisation.ReportService.Interface;
using ESFA.DC.Summarisation.ReportService.Service;

namespace ESFA.DC.Summarisation.ReportService.Modules
{
    public class ReportsServiceModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<ReportGenerationService>().As<IReportGenerationService>();
            containerBuilder.RegisterType<FileNameService>().As<IFileNameService>();
            containerBuilder.RegisterType<CsvFileService>().As<ICsvFileService>();
        }
    }
}