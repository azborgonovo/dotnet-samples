using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Autofac;
using Autofac.Integration.Wcf;
using WcfWithAutofac.WcfServiceLibrary;
using WcfWithAutofac.WcfServiceLibrary.DomainServices;

namespace WcfWithAutofac.ConsoleAppHost
{
    class Program
    {
        private static string baseAddress = "http://localhost:8000/WcfWithAutofac/Service";
        private static string serviceEndpointAddress = "CalculatorService";

        static void Main(string[] args)
        {
            // Step 0 Build DI container with Autofac
            var container = BuildContainer();

            // Step 1 Create a URI to serve as the base address.  
            var baseAddressUri = new Uri(baseAddress);

            // Step 2 Create a ServiceHost instance  
            var selfHost = new ServiceHost(typeof(CalculatorService), baseAddressUri);

            try
            {
                // Step 3 Add a service endpoint. OPTIONAL when using .NET Framework 4 or later
                selfHost.AddServiceEndpoint(typeof(ICalculatorService), new WSHttpBinding(), serviceEndpointAddress);

                // Step 3.1 (AUTOFAC) - Here's the important part - attaching the DI behavior to
                // the service host and passing in the container.
                selfHost.AddDependencyInjectionBehavior<ICalculatorService>(container);

                // Step 4 Enable metadata exchange.  
                var smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);

                // Step 5 Start the service.  
                selfHost.Open();
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();

                // Close the ServiceHostBase to shutdown the service.  
                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);

                Console.WriteLine("Press <ENTER> to abort service.");
                Console.WriteLine();
                Console.ReadLine();

                selfHost.Abort();
            }
        }

        static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            // Domain Services
            builder.RegisterType<Calculator>().AsImplementedInterfaces();

            // WCF Services
            builder.RegisterType<CalculatorService>().AsImplementedInterfaces();

            return builder.Build();
        }
    }
}
