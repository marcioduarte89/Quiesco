namespace Availability.Infrastructure.Data.Configuration
{
    using ClassMaps;
    using Indexes;
    using System.Threading.Tasks;
    using Conventions;
    using Serializers;

    /// <summary>
    /// Configures mongo instance
    /// </summary>
    public class MongoConfiguration
    {
        /// <summary>
        /// Initialises mongo configuration
        /// </summary>
        /// <param name="connectionString">Database connection</param>
        /// <param name="database">database name</param>
        /// <returns></returns>
        public async Task Init(string connectionString, string database)
        {
            // Don't invert the order of the registrations
            // we need to load map registration before checking and assigning indexes configuration
            new GlobalSerializersRegistration().Load();
            new ConventionRegistration().Load();
            new ClassMapRegistration().Load();
            await new IndexConfiguration().Init(connectionString, database);
            
        }
    }
}
