﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using System.Collections.Generic;
using System.Linq;

namespace SharedKernel.Bus.Extensions
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
            var endPointName = configuration.GetValue<string>(ENDPOINT_NAME);

            var routing = configuration.GetSection("Routes").Get<IEnumerable<Route>>();

            var bootstrapper = new Bootstrapper(endPointName, connectionString);

            if(routing != null && routing.Any())
            {
                bootstrapper.InitialiseRouting(routing);
            }
            
            return bootstrapper.EndpointConfiguration;
        }
    }
}
