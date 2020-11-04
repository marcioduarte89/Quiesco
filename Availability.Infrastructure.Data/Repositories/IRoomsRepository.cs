namespace Availability.Infrastructure.Data.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Models;

    /// <summary>
    /// Rooms repository Interface
    /// </summary>
    public interface IRoomsRepository
    {
        /// <summary>
        /// Gets the room with property and room ids
        /// </summary>
        /// <param name="propertyId">property id</param>
        /// <param name="roomId">room id</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of the <see cref="Room"/></returns>
        Task<Room> Get(int propertyId, int roomId, CancellationToken cancellationToken);

        /// <summary>
        /// Saves a room
        /// </summary>
        /// <param name="room">room details</param>
        /// <param name="cancellationToken"></param>
        Task Save(Room room, CancellationToken cancellationToken);
    }
}
