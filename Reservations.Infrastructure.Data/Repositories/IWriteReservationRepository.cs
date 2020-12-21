
namespace Reservations.Infrastructure.Data.Repositories
{
    using Reservations.Core.Models;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IWriteReservationRepository {

        Task Save(Reservation reservation, CancellationToken cancellation);
    }
}
