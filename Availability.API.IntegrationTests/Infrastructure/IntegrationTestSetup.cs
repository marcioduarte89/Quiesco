namespace Availability.API.IntegrationTests
{
    using Docker.DotNet;
    using Infrastructure;
    using Infrastructure.Containers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.SqlServer.Dac;
    using NUnit.Framework;
    using System;
    using System.Data.SqlClient;
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
                UpdateConfig("ConnectionStrings:Main", "mongodb://127.0.0.1:27017");
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
                UpdateConfig("ConnectionStrings:Main", "mongodb://127.0.0.1:27018");
            }

            var nsbConnectionString = GetConnectionString(_config.GetConnectionString("Nsb"), "Quiesco.NSB.Tests");
            DeployDb(nsbConnectionString, "Quiesco.Nsb.DB.dacpac");

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


        private void DeployDb(string connectionString, string dacpacFileName)
        {
            var dbName = GetDbName(connectionString);

            var dacOptions = new DacDeployOptions
            {
                BlockOnPossibleDataLoss = false,
                CreateNewDatabase = true
            };

            TestContext.Progress.WriteLine($"Deploying {dbName}...");

            var dacServiceInstance = new DacServices(connectionString);

            var basePath = TestContext.CurrentContext.TestDirectory;
            var dacpacFile = Path.Combine(basePath, dacpacFileName);
            using (var dacPac = DacPackage.Load(dacpacFile))
            {
                dacServiceInstance.Deploy(dacPac, dbName, true, dacOptions);
            }
        }

        private void DropDb(string connectionString)
        {
            var dbName = GetDbName(connectionString);
            TestContext.Progress.WriteLine($"Dropping {dbName}...");

            // use "original" connection string, otherwise DB will be in use
            using (var cnn = new SqlConnection(UseMasterDb(connectionString)))
            {
                cnn.Open();
                using (var cm = cnn.CreateCommand())
                {
                    cm.CommandText = $"ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; "
                                     + $"DROP DATABASE [{dbName}]";

                    cm.ExecuteNonQuery();
                }
            }
        }

        private string GetDbName(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            return builder.InitialCatalog;
        }

        private string UseMasterDb(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = "master"
            };

            return builder.ConnectionString;
        }

        private string GetConnectionString(string connectionString, string catalog)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);

            // Need to change this so is agnostic of who's running it
            if (!bool.TryParse(isTestingEnv, out var result) || !result)
            {
                if (!builder.InitialCatalog.Equals(catalog))
                {
                    builder.InitialCatalog = catalog;
                }
            }

            return builder.ConnectionString;
        }

        private void UpdateConfig(string section, string connectionString)
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

                SetSectionPath(section, connectionString);
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);
            }
            catch (Exception ex)
            {
                TestContext.Progress.WriteLine("Error writing app settings", ex);
            }
        }
    }
}
