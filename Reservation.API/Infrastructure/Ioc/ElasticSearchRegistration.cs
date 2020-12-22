using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Reservations.Core.Models;
using SharedKernel.Search.Configuration;
using SharedKernel.Search.Models;

namespace Reservation.API.Infrastructure.Ioc
{
    /// <summary>
    /// Elastic Search Registration
    /// </summary>
    public class ElasticSearchRegistration : Module
    {
        /// <summary>
        /// Loads the container builder and registers generic components
        /// </summary>
        /// <param name="builder">Container builder</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var environment = context.Resolve<IWebHostEnvironment>();
                var searchConfiguration = configuration.GetSection("SearchConfiguration").Get<SearchConfiguration>();
                searchConfiguration.EnvironmentName = environment.EnvironmentName; // need a better place to handler this

                var elasticClient = ElasticClientSetup
                    .Initialise(searchConfiguration)
                    .ConnectionsSettingsSetup()
                    .AddClientMappings<Room>(x =>
                        x.IndexName("properties")
                        .IdProperty(p => p.RoomId) // this needs to be fixed using a UId instead of an id...
                    ).Create();

                return elasticClient;
            }).AsImplementedInterfaces()
            .SingleInstance();

        }
    }
}
