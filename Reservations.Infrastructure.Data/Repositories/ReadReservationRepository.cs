namespace Reservations.Infrastructure.Data.Repositories
{
    using MongoDB.Driver;
    using Reservations.Core.Models;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Reservation repository exclusively for read operations
    /// </summary>
    public class ReadReservationRepository : RepositoryBase, IReadReservationRepository
    {
        private readonly IMongoCollection<Reservation> _reservationCollection;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mongoDatabase"></param>
        public ReadReservationRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
            _reservationCollection = GetTypeCollection<Reservation>();
        }

        /// <summary>
        /// Gets the reservation by reservation id
        /// </summary>
        /// <param name="reservationId">reservation id</param>
        /// <param name="cancellation"></param>
        /// <returns>Retuns the <see cref="Reservation"/> by reservation id</returns>
        public async Task<Reservation> Get(Guid reservationId, CancellationToken cancellation)
        {
            var query = $"{{ _id: {reservationId} }}";
            var reservation = (await _reservationCollection.FindAsync(query, cancellationToken: cancellation)).FirstOrDefault();

            return reservation;
        }
    }
}
