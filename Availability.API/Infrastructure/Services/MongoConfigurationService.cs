namespace Availability.API.Infrastructure.Services
{
    using Availability.Infrastructure.Data.Configuration.Indexes;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using SharedKernel.Mongo.ClassMaps;
    using SharedKerner.Mongo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Configures MongoDB instance
    /// </summary>
    public class MongoConfigurationService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private const string INFRASTRUCTURE_ASSEMBLY_NAME = "Availability.Infrastructure.Data";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">App configuration</param>
        public MongoConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var mainConnectionString = _configuration.GetConnectionString("Main");
            var database = _configuration.GetConnectionString("Database");

            await new MongoConfiguration(mainConnectionString, database)
                .RegisterConventions()
               .RegisterClassMaps(GetClassMaps())
               .RegisterGlobalSerializers()
               .ConfigureCoumpoundIndexes("rooms", RoomIndexes.CreateRoomCompoundIndex(), RoomIndexes.COMPOUND_INDEX);
        }

        private IEnumerable<Type> GetClassMaps()
        {
            var classMaps = Assembly.Load(INFRASTRUCTURE_ASSEMBLY_NAME).GetTypes()
                 .Where(mytype => mytype.GetInterfaces().Contains(typeof(IClassMapRegistration))).ToList();

            return classMaps;
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
