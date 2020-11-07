using Microsoft.Extensions.Configuration;
using NServiceBus;
using SharedKernel.Bus;
using System.Collections.Generic;

namespace Reservations.API.Infrastructure
{
    /// <summary>
    /// Reservations NServicebus bootstrapper
    /// </summary>
    public class NServiceBusBootstrapper
    {
        private const string ENDPOINT_NAME = "Reservations";

        /// <summary>
        /// Instance registered in the container, to be injected where required 
        /// </summary>
        internal static IEndpointInstance Endpoint;

        /// <summary>
        /// Configure and start <see cref="Endpoint"/>
        /// </summary>
        internal static void StartEndpoint(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Nsb");
            var routing = configuration.GetSection("Routes").Get<IEnumerable<Route>>();

            var bootstrapper = new Bootstrapper(ENDPOINT_NAME, connectionString, routing);
            Endpoint = bootstrapper.Start().GetAwaiter().GetResult();
        }

        internal static void StopEndpoint()
        {
            Endpoint.Stop().GetAwaiter().GetResult(); ;
        }
    }
}
