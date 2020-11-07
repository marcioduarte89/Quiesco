using Microsoft.Extensions.Configuration;
using NServiceBus;
using SharedKernel.Bus;
using System.Collections.Generic;

namespace Availability.API.Infrastructure
{
    /// <summary>
    /// Reservations NServicebus bootstrapper
    /// </summary>
    public class NServiceBusBootstrapper
    {
        private const string ENDPOINT_NAME = "Availability";

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

            var bootstrapper = new Bootstrapper(ENDPOINT_NAME, connectionString);
            Endpoint = bootstrapper.Start().GetAwaiter().GetResult();
        }

        internal static void StopEndpoint()
        {
            Endpoint.Stop().GetAwaiter().GetResult(); ;
        }
    }
}
