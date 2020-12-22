namespace Reservations.Infrastructure.Data.Repositories
{
    using MongoDB.Driver;
    using Reservations.Core.Models;
    using System.Threading;
    using System.Threading.Tasks;

    public class WriteReservationRepository : RepositoryBase, IWriteReservationRepository
    {
        private readonly IMongoCollection<Reservation> _reservationCollection;

        public WriteReservationRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
            _reservationCollection = GetTypeCollection<Reservation>();
        }

        /// <summary>
        /// Saves a reservation
        /// </summary>
        /// <param name="reservation">reservation details</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Creates or updates a <see cref="Reservation"/></returns>
        public async Task Save(Reservation reservation, CancellationToken cancellationToken)
        {
            await _reservationCollection.ReplaceOneAsync(
                x => x.RoomId == reservation.RoomId &&
                x.PropertyId == reservation.PropertyId &&
                x.ReservationId == reservation.ReservationId,
                reservation,
                new ReplaceOptions()
                {
                    IsUpsert = true
                },
                cancellationToken);
        }
    }
}
