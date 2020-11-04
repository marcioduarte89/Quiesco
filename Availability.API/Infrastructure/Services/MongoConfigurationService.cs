namespace Availability.API.Infrastructure.Services
{
    using Microsoft.Extensions.Hosting;
    using System.Threading;
    using System.Threading.Tasks;
    using Availability.Infrastructure.Data.Configuration;
    using Microsoft.Extensions.Configuration;

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
            await new MongoConfiguration().Init(mainConnectionString, database);
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
