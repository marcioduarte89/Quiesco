namespace Availability.API.IntegrationTests
{
    using Autofac;
    using Availability.API.IntegrationTests.Mocks;
    using Availability.Infrastructure.Data.Repositories;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    public class TestStartup : Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="environment">environment</param>
        public TestStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {
        }

        public override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);
            builder.RegisterType<GlobalMockRepository>().As<IGlobalReadRepository>().InstancePerLifetimeScope();
        }

    }
}
