namespace Availability.API.IntegrationTests.Infrastructure
{
    using System.IO;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    public class IntegrationTestsApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        /// <summary>
        /// Creates a Microsoft.Extensions.Hosting.IHostBuilder used to set up Microsoft.AspNetCore.TestHost.TestServer.
        /// </summary>
        /// <returns></returns>
        protected override IHostBuilder CreateHostBuilder()
        {

            return Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(x => {
                    x.UseTestServer()
                        .UseStartup<TStartup>();
                })
                .ConfigureAppConfiguration((hostingContext, config) => {
                    config.Sources.Clear();

                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true); ;
                });
            ;
        }
    }
}
