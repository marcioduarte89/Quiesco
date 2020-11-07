using NServiceBus;
using SharedKernel.Messages.Commands;
using System.Threading.Tasks;

namespace Availability.API.Features
{
    public class ExampleHandler : IHandleMessages<Example>
    {
        public Task Handle(Example message, IMessageHandlerContext context)
        {
            var tt = 1;
            return Task.CompletedTask;
        }
    }
}
