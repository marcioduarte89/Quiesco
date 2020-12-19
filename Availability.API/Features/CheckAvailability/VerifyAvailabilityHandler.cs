namespace Availability.API.Features.CheckAvailability
{
    using Availability.Infrastructure.Data.Repositories;
    using NServiceBus;
    using SharedKernel.Messages.Commands.Reservation;
    using SharedKernel.Messages.Events.Reservation;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Verifies Availability
    /// </summary>
    public class VerifyAvailabilityHandler : IHandleMessages<VerifyAvailability>
    {
        private IRoomRepository _repository;
        private readonly IGlobalReadRepository _globalReadRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">Room repository</param>
        /// <param name="globalReadRepository">Global read repository</param>
        public VerifyAvailabilityHandler(IRoomRepository repository, IGlobalReadRepository globalReadRepository)
        {
            _repository = repository;
            _globalReadRepository = globalReadRepository;
        }

        /// <summary>
        /// Handles requests of type <see cref="VerifyAvailability"/>
        /// </summary>
        /// <param name="message">Message type</param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(VerifyAvailability message, IMessageHandlerContext context)
        {
            var availability = new AvailabilityVerified()
            {
                HasAvailability = false,
                ReservationId = message.ReservationId
            };

            if (await _globalReadRepository.HasRoom(message.PropertyId, message.RoomId, CancellationToken.None))
            {
                var result = await _repository.CheckAvailability(message.PropertyId, message.RoomId, message.CheckIn, message.CheckOut, CancellationToken.None);
                availability.HasAvailability = result;
            }

            await context.Publish(availability);
        }
    }
}
