using MediatR;

namespace MediatorSampleWithMediatR.ConsoleApp.Commands
{
    public class PingCommand : IRequest<string>
    {
        public string Input { get; set; }
    }
}
