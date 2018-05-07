using MediatR;

namespace MediatorSampleWithMediatR.ConsoleApp.Commands
{
    public class OneWayCommand : IRequest
    {
        public string Input { get; set; }
    }
}
