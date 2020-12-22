namespace Reservation.API.Features.CompleteReservation
{
    using NServiceBus;
    using Reservation.Common.Exceptions;
    using Reservations.Infrastructure.Data.Repositories;
    using SharedKernel.Messages.Commands.Reservation;
    using SharedKernel.Messages.Events.Reservation;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Completes the reservation
    /// </summary>
    public class CompleteReservationHandler : IHandleMessages<CreateReservation>
    {
        private readonly IReadReservationRepository _readRepository;
        private readonly IWriteReservationRepository _writeRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="readRepository">Read Reservation repository</param>
        /// <param name="writeRepository">Write Reservation repository</param>
        public CompleteReservationHandler(IReadReservationRepository readRepository, IWriteReservationRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        /// <summary>
        /// Handles the completion of the reservation
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(CreateReservation message, IMessageHandlerContext context)
        {
            var reservation = await _readRepository.Get(message.ReservationId, CancellationToken.None);

            if (reservation == null)
            {
                throw new ReservationException(ReservationException.NOT_FOUND, $"Cannot find reservation with id {message.ReservationId}");
            }

            reservation.CompleteReservation();
            await _writeRepository.Save(reservation, CancellationToken.None);

            await context.Publish(new ReservationCompleted()
            {
                ReservationId = message.ReservationId
            });
        }
    }
}
