namespace Availability.Infrastructure.Data.Repositories
{
    using Core.Models;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Rooms repository
    /// </summary>
    public class RoomRepository : RepositoryBase, IRoomRepository
    {
        private readonly IMongoCollection<Room> _roomCollection;

        public RoomRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
            _roomCollection = GetTypeCollection<Room>();
        }

        /// <summary>
        /// Gets the room with property and room ids
        /// </summary>
        /// <param name="propertyId">property id</param>
        /// <param name="roomId">room id</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of the <see cref="Room"/></returns>
        public async Task<Room> Get(int propertyId, int roomId, CancellationToken cancellationToken)
        {
            // Other ways of queries the data
            //var filter = new FilterDefinitionBuilder<Room>().Where(x => x.PropertyId == propertyId && x.RoomId == roomId);
            //var filter = "{ propertyId: 1, roomId: 1}";
            // var room = (await collection.FindAsync(filter, cancellationToken: cancellationToken)).FirstOrDefault();
            //var filter = Builders<Room>.Filter.AnyEq("BookedSlots", new BsonDocument { { "FromDate", 20201019 }});
            //var tests = "{ BookedSlots: {$in: [ 20201019 ]} }";

            var filter = $"{{ propertyId: {propertyId}, roomId: {roomId} }}";
            return (await _roomCollection.FindAsync(filter, cancellationToken: cancellationToken)).FirstOrDefault();
        }

        /// <summary>
        /// Checks Room availability
        /// </summary>
        /// <param name="propertyId">Property id</param>
        /// <param name="roomId">Room id</param>
        /// <param name="checkIn">Check-in date</param>
        /// <param name="checkOut">Check-out date</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns whether a room is available for the given slot</returns>
        public async Task<bool> CheckAvailability(int propertyId, int roomId,
             DateTime checkIn, DateTime checkOut, CancellationToken cancellationToken)
        {
            var reservationSlot = new List<DateTime>();
            for (var date = checkIn; date <= checkOut; date = date.Date.AddDays(1))
            {
                reservationSlot.Add(date);
            }

            var result =  (await _roomCollection.FindAsync(x => x.BookedSlots.Any(y => reservationSlot.Any(z => z.CompareTo(y.Date) == 0)), cancellationToken: cancellationToken)).FirstOrDefault();

            var bookedSloteds = $"{{ propertyId: {propertyId}, roomId: {roomId}, BookedSlots: {{$in: [ {string.Join(',', reservationSlot) }}} }}";
            var tttt = (await _roomCollection.FindAsync(bookedSloteds, cancellationToken: cancellationToken)).FirstOrDefault();


            return false;
        }

        /// <summary>
        /// Saves a room
        /// </summary>
        /// <param name="room">room details</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of <see cref="Room"/> of the Saved room</returns>
        public async Task Save(Room room, CancellationToken cancellationToken)
        {
            await _roomCollection.ReplaceOneAsync(
                x => x.RoomId == room.RoomId && x.PropertyId == room.PropertyId, 
                room, 
                new ReplaceOptions()
                {
                    IsUpsert = true
                },
                cancellationToken);
        }
    }
}
