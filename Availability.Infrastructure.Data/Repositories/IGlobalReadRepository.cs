namespace Availability.Infrastructure.Data.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Quiesco global repository
    /// </summary>
    public interface IGlobalReadRepository
    {
        /// <summary>
        /// Checks if the Rooms exists in the global repository (Elastic search)
        /// </summary>
        /// <param name="propertyId">property id</param>
        /// <param name="roomId">room id</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns whether the room exists globally</returns>
        Task<bool> HasRoom(int propertyId, int roomId, CancellationToken cancellationToken);
    }
}
