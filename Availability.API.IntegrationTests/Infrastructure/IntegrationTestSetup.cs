namespace Availability.API.IntegrationTests
{
    using Docker.DotNet;
    using Infrastructure;
    using Infrastructure.Containers;
    using Microsoft.Extensions.Configuration;
    using NUnit.Framework;
    using SharedKernel.Tests.Deployment;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    [SetUpFixture]
    public class IntegrationTestSetup
    {
        private string _nsbConnectionString;
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
                DatabaseDeployment.UpdateConfig("ConnectionStrings:Main", "mongodb://127.0.0.1:27017");
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
                DatabaseDeployment.UpdateConfig("ConnectionStrings:Main", "mongodb://127.0.0.1:27018");
            }

            _nsbConnectionString = DatabaseDeployment.GetConnectionString(_config.GetConnectionString("Nsb"), "Quiesco.NSB.Tests");
            DatabaseDeployment.DeployDb(_nsbConnectionString, "Quiesco.Nsb.DB.dacpac");

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
            DatabaseDeployment.DropDb(_nsbConnectionString);
        }
    }
}
