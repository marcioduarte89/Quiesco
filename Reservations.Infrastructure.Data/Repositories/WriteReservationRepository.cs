
namespace Reservations.Infrastructure.Data.Repositories
{
    using Reservations.Core.Models;
    using System.Threading;
    using System.Threading.Tasks;

    public class WriteReservationRepository : IWriteReservationRepository{

        public Task Save(Reservation reservation, CancellationToken cancellation)
        {

        }
    }
}
