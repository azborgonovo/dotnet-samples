using MediatorSampleWithMediatR.ConsoleApp.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorSampleWithMediatR.ConsoleApp.CommandHandlers
{
    public class PingCommandHandler : IRequestHandler<PingCommand, string>
    {
        public Task<string> Handle(PingCommand command, CancellationToken cancellationToken)
        {
            if (!string.Equals(command.Input, "ping", StringComparison.InvariantCultureIgnoreCase))
                throw new ArgumentException("Expected value is 'ping'.", nameof(command.Input));

            return Task.FromResult("Pong");
        }
    }
    
    // For convenience of handlers that do not need the use of the cancellation token, inherit from the AsyncRequestHandler base class
    //public class PingCommandHandler : AsyncRequestHandler<PingCommand, string>
    //{
    //    protected override Task<string> HandleCore(PingCommand request)
    //    {
    //        return Task.FromResult("Pong");
    //    }
    //}

    // Or if the request is completely synchronous, inherit from the base RequestHandler class
    //public class SyncHandler : RequestHandler<PingCommand, string>
    //{
    //    protected override string Handle(PingCommand request)
    //    {
    //        return "Pong";
    //    }
    //}
}
