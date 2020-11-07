namespace Availability.API.Infrastructure.Service
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// NServiceBus Service
    /// </summary>
    public class NServiceBusService : IHostedService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">App configuration</param>
        public NServiceBusService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        public async Task StartAsync(CancellationToken cancellationToken) => NServiceBusBootstrapper.StartEndpoint(_configuration);

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        public async Task StopAsync(CancellationToken cancellationToken) => NServiceBusBootstrapper.StopEndpoint();
    }
}
