using MediatR;

namespace MediatorSampleWithMediatR.ConsoleApp.Events
{
    public class UserInputContainsImportantWordEvent : INotification
    {
        public UserInputContainsImportantWordEvent()
        {
        }
    }
}
