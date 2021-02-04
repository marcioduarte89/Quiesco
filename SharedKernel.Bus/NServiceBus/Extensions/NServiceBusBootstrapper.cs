using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using SharedKernel.Bus.NServiceBus.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedKernel.Bus.NServiceBus.Extensions
{
    /// <summary>
    /// Reservations NServicebus bootstrapper
    /// </summary>
    public static class NServiceBusBootstrapper
    {
        private const string ENDPOINT_NAME = "EndpointName";
        private const string NSB_CONNECTIONSTRING_NAME = "Nsb";

        /// <summary>
        /// Configures NServiceBus
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IHostBuilder ConfigureNServiceBus(this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseNServiceBus(hostBuilderContext =>
            {
                return Configure(hostBuilderContext.Configuration);
            });
        }

        /// <summary>
        /// Configure and start <see cref="Endpoint"/>
        /// </summary>
        private static EndpointConfiguration Configure(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(NSB_CONNECTIONSTRING_NAME);

            var nServiceBusConfiguration = configuration.GetSection("NServiceBusConfiguration").Get<Configuration>();

            var bootstrapper = new Bootstrapper(nServiceBusConfiguration.EndpointName, connectionString);

            if(nServiceBusConfiguration.Routes != null && nServiceBusConfiguration.Routes.Any())
            {
                bootstrapper.InitialiseRouting(nServiceBusConfiguration.Routes);
            }

            bootstrapper.ConfigureRecoverability();

            return bootstrapper.EndpointConfiguration;
        }
    }
}
