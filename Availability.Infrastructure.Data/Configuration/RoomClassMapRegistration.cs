namespace Availability.Infrastructure.Data.Configuration
{
    using Core.Models;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Serializers;

    public class RoomClassMapRegistration : IClassMapRegistration
    {
        public void Load()
        {
            BsonClassMap.RegisterClassMap<Room>(x =>
            {
                x.AutoMap();
                x.MapIdField(y => y.Id);
                x.MapField("_bookedSlots").SetElementName("BookedSlots");
                x.UnmapMember(y => y.BookedSlots);
            });

            BsonClassMap.RegisterClassMap<Price>(x =>
            {
                x.AutoMap();
            });
        }
    }
}
