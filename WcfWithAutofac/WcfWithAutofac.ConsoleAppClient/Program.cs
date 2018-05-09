using System;
using System.ServiceModel;
using Autofac;
using Autofac.Integration.Wcf;
using WcfWithAutofac.ConsoleAppClient.WcfWithAutofacServiceReference;

namespace WcfWithAutofac.ConsoleAppClient
{
    class Program
    {
        private static string eindpointAddress = "http://localhost:8000/WcfWithAutofac/Service/CalculatorService";

        static void Main(string[] args)
        {
            var container = BuildContainer();

            using (var lifetime = container.BeginLifetimeScope())
            {
                //Step 1: Create an instance of the gateway and the dependent WCF proxy.
                var calculatorGateway = lifetime.Resolve<ICalculatorGateway>();

                // Step 2: Call the service operations.  
                calculatorGateway.CallServiceOperations();
                
                //Step 3: Closing the client gracefully closes the connection and cleans up resources.  
                //client.Close();
            }

            Console.WriteLine();
            Console.WriteLine("Press <ENTER> to terminate the client.");
            Console.WriteLine();
            Console.ReadLine();
        }

        static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            // Register the channel factory for the service. Make it
            // SingleInstance since you don't need a new one each time.
            builder
                .Register(c => new ChannelFactory<ICalculatorService>(
                    new WSHttpBinding(),
                    new EndpointAddress(eindpointAddress)))
                .SingleInstance();

            // Register the service interface using a lambda that creates
            // a channel from the factory. Include the UseWcfSafeRelease()
            // helper to handle proper disposal.
            builder
                .Register(c => c.Resolve<ChannelFactory<ICalculatorService>>().CreateChannel())
                .As<ICalculatorService>()
                .UseWcfSafeRelease();

            // You can also register other dependencies.
            builder.RegisterType<CalculatorGateway>().AsImplementedInterfaces();

            return builder.Build();
        }
    }
}
