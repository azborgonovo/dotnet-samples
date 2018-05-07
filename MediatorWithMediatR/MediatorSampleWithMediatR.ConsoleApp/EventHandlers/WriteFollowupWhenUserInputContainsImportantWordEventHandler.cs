using MediatorSampleWithMediatR.ConsoleApp.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorSampleWithMediatR.ConsoleApp.EventHandlers
{
    public class WriteFollowupWhenUserInputContainsImportantWordEventHandler : INotificationHandler<UserInputContainsImportantWordEvent>
    {
        public Task Handle(UserInputContainsImportantWordEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Your input contains the word 'important'. It will be followed-up.");
            return Task.CompletedTask;
        }
    }
}
