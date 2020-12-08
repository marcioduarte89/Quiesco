namespace Availability.Infrastructure.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Models;

    /// <summary>
    /// Room repository Interface
    /// </summary>
    public interface IRoomRepository
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
        /// Checks Room availability
        /// </summary>
        /// <param name="propertyId">Property id</param>
        /// <param name="roomId">Room id</param>
        /// <param name="checkIn">Check-in date</param>
        /// <param name="checkOut">Check-out date</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns whether a room is available for the given slot</returns>
        Task<bool> CheckAvailability(int propertyId, int roomId, DateTime checkIn, DateTime checkOut, CancellationToken cancellationToken);

        /// <summary>
        /// Saves a room
        /// </summary>
        /// <param name="room">room details</param>
        /// <param name="cancellationToken"></param>
        Task Save(Room room, CancellationToken cancellationToken);
    }
}
