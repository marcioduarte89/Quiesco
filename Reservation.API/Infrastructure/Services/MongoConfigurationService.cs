namespace Reservations.API.Infrastructure.Services
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using SharedKerner.Mongo;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Configures MongoDB instance
    /// </summary>
    public class MongoConfigurationService : IHostedService
    {
        private readonly IConfiguration _configuration;

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

            new MongoConfiguration(mainConnectionString, database)
               .RegisterConventions()
               .RegisterGlobalSerializers();
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
