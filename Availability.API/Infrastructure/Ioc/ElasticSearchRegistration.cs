using Autofac;
using Availability.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SharedKernel.Search.Configuration;
using SharedKernel.Search.Models;

namespace Availability.API.Infrastructure.Ioc
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

                var elasticClient = ElasticClientSetup.Initialise(searchConfiguration)
                .ConnectionsSettingsSetup()
                .AddClientMappings<Room>(x =>
                    x.IndexName("availability")
                    .IdProperty(p => p.Id)
                ).Create();

                return elasticClient;
            }).AsImplementedInterfaces()
            .SingleInstance();

        }
    }
}
