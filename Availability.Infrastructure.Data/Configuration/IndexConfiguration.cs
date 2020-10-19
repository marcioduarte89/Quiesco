namespace Availability.Infrastructure.Data.Configuration
{
    using System;
    using System.Linq;
    using Availability.Core.Models;
    using MongoDB.Driver;
    using System.Threading.Tasks;

    public class IndexConfiguration
    {
        public const string COMPOUND_INDEX = "propertyId_roomId_index";

        public async Task Init(string connectionString, string database)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(database);

            var roomsIndexKeyBuilder = Builders<Room>.IndexKeys;

            var compoundIndex = roomsIndexKeyBuilder.Ascending(x => x.RoomId).Ascending(x => x.PropertyId);
            var indexModel = new CreateIndexModel<Room>(compoundIndex, new CreateIndexOptions() { Name = COMPOUND_INDEX, Background = true });

            var existingIndexes = await (await db.GetCollection<Room>("rooms").Indexes.ListAsync()).ToListAsync();
            bool hasCompoundIndex = false;
            
            foreach (var index in existingIndexes.Where(index => index.Values.Any(x => x.Equals(COMPOUND_INDEX))))
            {
                hasCompoundIndex = true;
            }

            if (!hasCompoundIndex)
            {
                await db.GetCollection<Room>("rooms").Indexes.CreateOneAsync(indexModel);
            }
        }
    }
}
