namespace Availability.API.Features.CheckAvailability
{
    using Availability.Common.Exceptions;
    using Availability.Common.Extensions;
    using Availability.Infrastructure.Data.Repositories;
    using NServiceBus;
    using SharedKernel.Messages.Commands.Reservation;
    using SharedKernel.Messages.Events.Reservation;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Adds a booking
    /// </summary>
    public class AddBookingHandler : IHandleMessages<AddBooking>
    {
        private IRoomRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">Room repository</param>
        public AddBookingHandler(IRoomRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Handles requests of type <see cref="VerifyAvailability"/>
        /// </summary>
        /// <param name="message">Message type</param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(AddBooking message, IMessageHandlerContext context)
        {
            var existingRoom = await _repository.Get(message.PropertyId, message.RoomId, CancellationToken.None);

            if (existingRoom == null)
            {
                throw new AvailabilityException(AvailabilityException.INVALID_DATA, $"The room with id {message.RoomId} in property id {message.PropertyId} does not already exists");
            }

            var availability = existingRoom.HasAvailability(message.CheckIn, message.CheckOut);

            if (!availability)
            {
                await context.Publish(new AddBookingFailed()
                {
                    ReservationId = message.ReservationId,
                    Reason = "Room is not available"
                });

                return;
            }

            existingRoom.AddBookings(message.CheckIn.ToSlotList(message.CheckOut));

            await _repository.Save(existingRoom, CancellationToken.None);

            await context.Publish(new BookingAdded()
            {
                ReservationId = message.ReservationId
            });
        }
    }
}
