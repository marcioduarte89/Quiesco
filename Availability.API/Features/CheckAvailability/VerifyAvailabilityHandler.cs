namespace Availability.API.Features.CheckAvailability
{
    using Availability.Infrastructure.Data.Repositories;
    using NServiceBus;
    using SharedKernel.Messages.Commands;
    using SharedKernel.Messages.Events;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Verifies Availability
    /// </summary>
    public class VerifyAvailabilityHandler : IHandleMessages<VerifyAvailability>
    {
        private IRoomRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public VerifyAvailabilityHandler(IRoomRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Handles requests of type <see cref="VerifyAvailability"/>
        /// </summary>
        /// <param name="message">Message type</param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(VerifyAvailability message, IMessageHandlerContext context)
        {
            var result = await _repository.CheckAvailability(message.PropertyId, message.RoomId, message.CheckIn, message.CheckOut, CancellationToken.None);

            await context.Publish(new AvailabilityVerified()
            {
                HasAvailability = true,
                ReservationId = message.ReservationId
            });
        }
    }
}
