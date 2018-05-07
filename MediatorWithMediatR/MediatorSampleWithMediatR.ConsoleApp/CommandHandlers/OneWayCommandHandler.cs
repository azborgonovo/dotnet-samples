using MediatorSampleWithMediatR.ConsoleApp.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorSampleWithMediatR.ConsoleApp.CommandHandlers
{
    public class OneWayCommandHandler : IRequestHandler<OneWayCommand>
    {
        public Task Handle(OneWayCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"One way command handled with input '{request.Input}'.");

            return Task.CompletedTask;
        }
    }
}
