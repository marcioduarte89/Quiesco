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
    using System.Threading.Tasks;

    [SetUpFixture]
    public class IntegrationTestSetup
    {
        private string _applicationConnectionString;
        private IConfiguration _config;
        private DockerClient _dockerClient;
        private MongoContainer _mongoContainer;
        private Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<TestStartup> _factory;
        private string isTestingEnv = Environment.GetEnvironmentVariable("CI");

        protected internal static HttpClient Client;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTests()
        {
            TestContext.Progress.WriteLine(isTestingEnv);

            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Integration.json")
                .Build();

            if (!bool.TryParse(isTestingEnv, out var result) || !result)
            {
                UpdateConfig("mongodb://127.0.0.1:27017");
                _dockerClient = new DockerClientConfiguration(
                    // TODO: This needs to be configurable in order to execute tests in CI
                    new Uri("npipe://./pipe/docker_engine"))
                .CreateClient();

                DockerContainerBase.CleanupOrphanedContainers(_dockerClient).Wait(30000);

                _mongoContainer = new MongoContainer();
                await _mongoContainer.Start(_dockerClient);
            }
            else
            {
                UpdateConfig("mongodb://127.0.0.1:27018");
            }

            _factory = new IntegrationTestsApplicationFactory<TestStartup>();

            Client = _factory.CreateClient();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            if (!bool.TryParse(isTestingEnv, out var result) && !result)
            {
                _mongoContainer.Remove(_dockerClient).Wait(30000);
            }
            _factory?.Dispose();
            Client?.Dispose();
        }

        private void UpdateConfig(string connectionString)
        {
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.Integration.json");
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
