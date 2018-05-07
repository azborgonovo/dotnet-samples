using Autofac;
using MediatorSampleWithMediatR.ConsoleApp.Commands;
using MediatorSampleWithMediatR.ConsoleApp.Events;
using MediatR;
using System;
using System.Collections.Generic;

namespace MediatorSampleWithMediatR.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Registering and resolving dependencies using Autofac
            var container = RegisterDependencies();
            var mediator = container.Resolve<IMediator>();


            // Request/response example
            RequestResponseExample(mediator);

            // One-way example
            OneWayExample(mediator);

            // Publishing example
            PublishingExample(mediator);


            // Freezing UI so we can check the result
            Console.ReadKey();
        }
        
        static IContainer RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // mediator itself
            builder
              .RegisterType<Mediator>()
              .As<IMediator>()
              .InstancePerLifetimeScope();

            // request handlers
            builder
              .Register<SingleInstanceFactory>(ctx => {
                  var c = ctx.Resolve<IComponentContext>();
                  return t => c.TryResolve(t, out var o) ? o : null;
              })
              .InstancePerLifetimeScope();

            // notification handlers
            builder
              .Register<MultiInstanceFactory>(ctx => {
                  var c = ctx.Resolve<IComponentContext>();
                  return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
              })
              .InstancePerLifetimeScope();

            // finally register our custom code (individually, or via assembly scanning)
            // - requests & handlers as transient, i.e. InstancePerDependency()
            // - pre/post-processors as scoped/per-request, i.e. InstancePerLifetimeScope()
            // - behaviors as transient, i.e. InstancePerDependency()

            // via assembly scan
            builder.RegisterAssemblyTypes(typeof(Program).Assembly).AsImplementedInterfaces();

            // or individually
            //builder.RegisterType<MyHandler>().AsImplementedInterfaces().InstancePerDependency();          

            var container = builder.Build();

            return container;
        }

        static void RequestResponseExample(IMediator mediator)
        {
            Console.WriteLine("===============================");
            Console.WriteLine("Request/response example");
            Console.WriteLine("===============================");
            Console.WriteLine("Please provide message content:");

            var userInput = Console.ReadLine();
            var userOutput = SendPingCommand(mediator, userInput);

            Console.WriteLine($"The system response is: {userOutput}");
            Console.WriteLine();
        }

        static void OneWayExample(IMediator mediator)
        {
            Console.WriteLine("===============================");
            Console.WriteLine("One-way example");
            Console.WriteLine("===============================");
            Console.WriteLine("Please provide message content:");

            string userInput = Console.ReadLine();
            FireAndForgetCommand(mediator, userInput);

            Console.WriteLine();
        }

        static void PublishingExample(IMediator mediator)
        {
            Console.WriteLine("===============================");
            Console.WriteLine("Publishing example");
            Console.WriteLine("===============================");
            Console.WriteLine("Please provide some input (type 'important' to see a response):");

            string userInput = Console.ReadLine();
            if (userInput != null && userInput.ToLower().Contains("important"))
            {
                PublishEvent(mediator);
            }

            Console.WriteLine();
        }

        static string SendPingCommand(IMediator mediator, string input)
        {
            var pingCommand = new PingCommand() { Input = input };
            string userOutput;
            try
            {
                var response = mediator.Send(pingCommand);
                userOutput = response.Result;
            }
            catch (ArgumentException ex)
            {
                userOutput = ex.Message;
            }

            return userOutput;
        }

        static void FireAndForgetCommand(IMediator mediator, string input)
        {
            var oneWayCommand = new OneWayCommand() { Input = input };
            mediator.Send(oneWayCommand);
        }

        static void PublishEvent(IMediator mediator)
        {
            mediator.Publish(new UserInputContainsImportantWordEvent());
        }
    }
}
