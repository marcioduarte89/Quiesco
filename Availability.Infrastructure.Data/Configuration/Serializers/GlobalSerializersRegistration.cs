namespace Availability.Infrastructure.Data.Configuration.Serializers
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Serializers;

    /// <summary>
    /// Global serializers
    /// </summary>
    public class GlobalSerializersRegistration
    {
        /// <summary>
        /// Loads serializers
        /// </summary>
        public void Load()
        {
            BsonSerializer.RegisterSerializer(new DecimalSerializer(BsonType.Decimal128));
        }
    }
}
