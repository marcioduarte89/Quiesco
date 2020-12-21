namespace Availability.Infrastructure.Data.Configuration.ClassMaps
{
    using Core.Models;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.IdGenerators;
    using SharedKernel.Mongo.ClassMaps;

    /// <summary>
    /// Room class map registrations
    /// </summary>
    public class RoomClassMapRegistration : IClassMapRegistration
    {
        /// <summary>
        /// Loads class map registrations
        /// </summary>
        public void Load()
        {
            BsonClassMap.RegisterClassMap<Room>(x =>
            {
                x.AutoMap();
                x.MapIdField(y => y.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
                x.MapField("_bookedSlots").SetElementName("bookedSlots");
                x.UnmapMember(y => y.BookedSlots);
            });

            BsonClassMap.RegisterClassMap<Price>(x =>
            {
                x.AutoMap();
                x.MapProperty(y => y.Value);
                x.MapProperty(y => y.Date);
            });
        }
    }
}
