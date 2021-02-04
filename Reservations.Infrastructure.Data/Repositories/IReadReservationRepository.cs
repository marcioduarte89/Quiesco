namespace Reservations.Infrastructure.Data.Repositories
{
    using Reservations.Core.Models;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Reservation repository exclusively for read operations
    /// </summary>
    public interface IReadReservationRepository
    {
        /// <summary>
        /// Gets the reservation by reservation id
        /// </summary>
        /// <param name="reservationId">reservation id</param>
        /// <param name="cancellation"></param>
        /// <returns>Retuns the <see cref="Reservation"/> by reservation id</returns>
        Task<Reservation> Get(Guid reservationId, CancellationToken cancellation);
    }
}
