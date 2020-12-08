using NServiceBus;
using SharedKernel.Messages.Commands;
using System.Threading.Tasks;
using Availability.Infrastructure.Data.Repositories;

namespace Availability.API.Features
{
    public class ExampleHandler : IHandleMessages<Example>
    {

        private IRoomRepository _repository;

        public ExampleHandler(IRoomRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(Example message, IMessageHandlerContext context)
        {
            var tt = 1;
            return Task.CompletedTask;
        }
    }
}
