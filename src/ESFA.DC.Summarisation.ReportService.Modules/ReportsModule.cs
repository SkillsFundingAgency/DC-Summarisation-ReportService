using Autofac;
using ESFA.DC.Summarisation.ReportService.Data;
using ESFA.DC.Summarisation.ReportService.Data.Interface;
using ESFA.DC.Summarisation.ReportService.Interface;
using ESFA.DC.Summarisation.ReportService.Report.PeriodSummary;

namespace ESFA.DC.Summarisation.ReportService.Modules
{
    public class ReportsModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<PeriodSummaryDataProvider>().As<IPeriodSummaryDataProvider>();
            containerBuilder.RegisterType<PeriodSummaryReport>().As<IReport>();
        }
    }
}