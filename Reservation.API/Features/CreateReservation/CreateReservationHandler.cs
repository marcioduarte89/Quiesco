namespace Reservation.API.Features.CreateReservation
{
    using NServiceBus;
    using Reservations.Core.Models;
    using Reservations.Infrastructure.Data.Repositories;
    using SharedKernel.Messages.Commands.Reservation;
    using SharedKernel.Messages.Events.Reservation;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates a new Reservation
    /// </summary>
    public class CreateReservationHandler : IHandleMessages<CreateReservation>
    {
        private readonly IWriteReservationRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">Reservation repository</param>
        public CreateReservationHandler(IWriteReservationRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Handles the creation of a new reservation
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(CreateReservation message, IMessageHandlerContext context)
        {
            var reservationProperties = ReservationProperties.Create(
                message.PropertyId,
                message.RoomId,
                message.CheckIn,
                message.CheckOut,
                message.NumberOfOccupants);

            var user = User.Create(message.User.UserEmail, message.User.Name, message.User.PhoneNumber);
            user.LastName = message.User.LastName;

            var reservation = Reservation.Create(message.ReservationId, reservationProperties, user);

            await _repository.Save(reservation, CancellationToken.None); // need to take care of these cancellation tokens

            await context.Publish(new ReservationCreated()
            {
                ReservationId = message.ReservationId
            });
        }
    }
}
