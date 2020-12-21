namespace Availability.Infrastructure.Data.Configuration.Indexes
{
    using Availability.Core.Models;
    using MongoDB.Driver;

    /// <summary>
    /// Index configuration for Room type
    /// </summary>
    public static class RoomIndexes
    {
        public const string COMPOUND_INDEX = "propertyId_roomId_index";

        public static CreateIndexModel<Room> CreateRoomCompoundIndex()
        {
            var roomsIndexKeyBuilder = Builders<Room>.IndexKeys;

            var compoundIndex = roomsIndexKeyBuilder.Ascending(x => x.RoomId).Ascending(x => x.PropertyId);
            var indexModel = new CreateIndexModel<Room>(compoundIndex, new CreateIndexOptions()
            {
                Name = COMPOUND_INDEX,
                Background = true,
                Unique = true
            });

            return indexModel;
        }
    }
}
