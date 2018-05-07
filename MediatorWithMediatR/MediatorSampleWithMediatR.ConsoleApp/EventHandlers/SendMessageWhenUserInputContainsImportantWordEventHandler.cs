using MediatorSampleWithMediatR.ConsoleApp.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorSampleWithMediatR.ConsoleApp.EventHandlers
{
    public class SendMessageWhenUserInputContainsImportantWordEventHandler : INotificationHandler<UserInputContainsImportantWordEvent>
    {
        public Task Handle(UserInputContainsImportantWordEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("A message was sent to your manager.");
            return Task.CompletedTask;
        }
    }
}
