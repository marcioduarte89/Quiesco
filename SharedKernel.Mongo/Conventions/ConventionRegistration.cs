namespace SharedKernel.Mongo.Conventions
{
    using MongoDB.Bson.Serialization.Conventions;

    /// <summary>
    /// Convention registration
    /// </summary>
    public class ConventionRegistration
    {
        /// <summary>
        /// Loads conventions
        /// </summary>
        public static void Load()
        {
            var pack = new ConventionPack
            {
                new CamelCaseElementNameConvention()
            };

            ConventionRegistry.Register(
                "Camel Case Convention",
                pack,
                t => true);
        }
    }
}
