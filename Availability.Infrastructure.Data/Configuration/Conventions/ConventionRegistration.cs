namespace Availability.Infrastructure.Data.Configuration.Conventions
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
        public void Load()
        {
            var pack = new ConventionPack
            {
                new CamelCaseElementNameConvention()
            };

            ConventionRegistry.Register(
                "Availability Conventions",
                pack,
                t => true);
        }
    }
}
