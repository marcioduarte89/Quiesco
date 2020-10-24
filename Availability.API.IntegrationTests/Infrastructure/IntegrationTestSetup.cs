namespace Availability.API.IntegrationTests
{
    using Docker.DotNet;
    using Infrastructure;
    using Infrastructure.Containers;
    using Microsoft.Extensions.Configuration;
    using NUnit.Framework;
    using System;
    using System.IO;
    using System.Net.Http;

    [SetUpFixture]
    public class IntegrationTestSetup
    {
        private string _applicationConnectionString;
        private IConfiguration _config;
        private DockerClient _dockerClient;
        private MongoContainer _mongoContainer;
        private Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<Startup> _factory;

        protected internal static HttpClient Client;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json")
                .Build();

            UpdateConfig();

            _dockerClient = new DockerClientConfiguration(
                    // TODO: This needs to be configurable in order to execute tests in CI
                    new Uri("npipe://./pipe/docker_engine"))
                .CreateClient();

            DockerContainerBase.CleanupOrphanedContainers(_dockerClient).Wait(30000);

            _mongoContainer = new MongoContainer();
            _mongoContainer.Start(_dockerClient).Wait(30000);

            _factory = new IntegrationTestsApplicationFactory<Startup>();
            Client = _factory.CreateClient();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            _mongoContainer.Remove(_dockerClient).Wait(30000);
            _factory?.Dispose();
            Client?.Dispose();
        }

        private void UpdateConfig()
        {
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.Development.json");
                var json = File.ReadAllText(filePath);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                void SetSectionPath(string key, string value)
                {
                    var sectionPath = key.Split(":")[0];
                    if (!string.IsNullOrEmpty(sectionPath))
                    {
                        var keyPath = key.Split(":")[1];
                        jsonObj[sectionPath][keyPath] = value;
                    }
                    else
                    {
                        jsonObj[sectionPath] = value; // if no sectionpath just set the value
                    }
                }

                SetSectionPath("ConnectionStrings:Main", MongoContainer.ConnectionString);
                string output =
                    Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}
