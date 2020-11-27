using Autofac;
using ESFA.DC.Summarisation.ReportService.Interface;
using ESFA.DC.Summarisation.ReportService.Service.CollectionTypeFormatters;

namespace ESFA.DC.Summarisation.ReportService.Modules
{
    public class CollectionTypeFormattersModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<NCSCollectionTypeFormatter>().As<ICollectionTypeFormatter>();
            containerBuilder.RegisterType<APPSCollectionTypeFormatter>().As<ICollectionTypeFormatter>();
            containerBuilder.RegisterType<ESFCollectionTypeFormatter>().As<ICollectionTypeFormatter>();
            containerBuilder.RegisterType<ILRCollectionTypeFormatter>().As<ICollectionTypeFormatter>();
        }
    }
}