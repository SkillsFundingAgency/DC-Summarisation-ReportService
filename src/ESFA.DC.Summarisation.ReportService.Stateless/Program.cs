using System;
using System.Diagnostics;
using System.Threading;
using Autofac;
using Autofac.Integration.ServiceFabric;
using ESFA.DC.JobContextManager.Interface;
using ESFA.DC.JobContextManager.Model;

namespace ESFA.DC.Summarisation.ReportService.Stateless
{
    internal class Program
    {
        private  static void Main()
        {
            try
            {
                // Setup Autofac
                ContainerBuilder builder = DIComposition.BuildContainer();

                // Register the Autofac magic for Service Fabric support.
                builder.RegisterServiceFabricSupport();

                // Register the stateless service.
                builder.RegisterStatelessService<ServiceFabric.Common.Stateless>("ESFA.DC.Summarisation2021.ReportService.StatelessType");

                using (var container = builder.Build())
                {
                    container.Resolve<IMessageHandler<JobContextMessage>>();

                    ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(ServiceFabric.Common.Stateless).Name);

                    // Prevents this host process from terminating so services keep running.
                    Thread.Sleep(Timeout.Infinite);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                ServiceEventSource.Current.ServiceHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
