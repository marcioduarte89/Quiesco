﻿namespace Availability.Infrastructure.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Models;
    using MongoDB.Driver;
    using System.Threading;
    using System.Threading.Tasks;
    using MongoDB.Bson;

    /// <summary>
    /// Rooms repository
    /// </summary>
    public class RoomsRepository : RepositoryBase, IRoomsRepository
    {
        private readonly IMongoCollection<Room> _roomCollection;

        public RoomsRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
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

            var filter = "{ propertyId: 1, roomId: 1}";
            return (await _roomCollection.FindAsync(filter, cancellationToken: cancellationToken)).FirstOrDefault();
        }

        /// <summary>
        /// Saves a room
        /// </summary>
        /// <param name="room">room details</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of <see cref="Room"/> of the saved room</returns>
        public async Task Save(Room room, CancellationToken cancellationToken)
        {
            await _roomCollection.InsertOneAsync(room, cancellationToken: cancellationToken);
        }
    }
}
