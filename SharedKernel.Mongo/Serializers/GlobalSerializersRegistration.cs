namespace SharedKernel.Mongo.Serializers
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
        public static void Load()
        {
            BsonSerializer.RegisterSerializer(new DecimalSerializer(BsonType.String));
        }
    }
}
