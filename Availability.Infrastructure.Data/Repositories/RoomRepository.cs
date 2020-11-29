namespace Availability.Infrastructure.Data.Repositories
{
    using Core.Models;
    using MongoDB.Driver;
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
