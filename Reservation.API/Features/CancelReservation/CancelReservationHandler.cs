namespace Reservation.API.Features.CancelReservation
{
    using NServiceBus;
    using Reservation.Common.Exceptions;
    using Reservations.Infrastructure.Data.Repositories;
    using SharedKernel.Messages.Commands.Reservation;
    using SharedKernel.Messages.Events.Reservation;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates a new Reservation
    /// </summary>
    public class CancelReservationHandler : IHandleMessages<CancelReservation>
    {
        private readonly IReadReservationRepository _readRepository;
        private readonly IWriteReservationRepository _writeRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="readRepository">Read Reservation repository</param>
        /// <param name="writeRepository">Write Reservation repository</param>
        public CancelReservationHandler(IReadReservationRepository readRepository, IWriteReservationRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        /// <summary>
        /// Handles the creation of a new reservation
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(CancelReservation message, IMessageHandlerContext context)
        {
            var reservation = await _readRepository.Get(message.ReservationId, CancellationToken.None);

            if(reservation == null)
            {
                throw new ReservationException(ReservationException.NOT_FOUND, $"Cannot find reservation with id {message.ReservationId}");
            }

            reservation.CancelReservation(message.Reason);
            await _writeRepository.Save(reservation, CancellationToken.None);

            await context.Publish(new ReservationCancelled()
            {
                ReservationId = message.ReservationId,
            });
        }
    }
}
