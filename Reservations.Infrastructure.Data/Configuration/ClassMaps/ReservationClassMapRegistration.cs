namespace Reservations.Infrastructure.Data.Configuration.ClassMaps.Configuration.ClassMaps
{
    using Core.Models;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.IdGenerators;
    using MongoDB.Bson.Serialization.Serializers;
    using SharedKernel.Mongo.ClassMaps;

    /// <summary>
    /// Room class map registrations
    /// </summary>
    public class ReservationClassMapRegistration : IClassMapRegistration
    {
        /// <summary>
        /// Loads class map registrations
        /// </summary>
        public void Load()
        {
            BsonClassMap.RegisterClassMap<Reservation>(x =>
            {
                x.AutoMap();
                x.MapIdField(y => y.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
                x.MapMember(c => c.Status).SetSerializer(new EnumSerializer<Status>(BsonType.Int32));
            });
        }
    }
}
