namespace Products.API.IntegrationTests.Infrastructure
{
    using System;
    using System.IO;
    using System.Net.Http;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using Microsoft.SqlServer.Dac;
    using NUnit.Framework;

    [SetUpFixture]
    public class IntegrationTestSetup
    {
        private string _applicationConnectionString;
        private IConfiguration _config;
        private Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<Startup> _factory;

        protected internal static HttpClient Client;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            _config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .AddJsonFile("appsettings.Development.json")
               .Build();

            _applicationConnectionString = EnsureUniqueDbName(_config.GetConnectionString("Main"));
            UpdateConfig();

            DeployDb(_applicationConnectionString, "Products.DB.dacpac");

            _factory = new IntegrationTestsApplicationFactory<Startup>();
            Client = _factory.CreateClient();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {

            _factory?.Dispose();
            Client?.Dispose();
            DropDb(_applicationConnectionString);
        }

        private void DeployDb(string connectionString, string dacpacFileName)
        {
            var dbName = GetDbName(connectionString);

            var dacOptions = new DacDeployOptions
            {
                BlockOnPossibleDataLoss = false,
                CreateNewDatabase = true
            };

            Console.WriteLine($"Deploying {dbName}...");

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
            Console.WriteLine($"Dropping {dbName}...");

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
        private string EnsureUniqueDbName(string connectionString)
        {
            var suffix = Guid.NewGuid().ToString().Replace("-", "").ToUpper();

            var builder = new SqlConnectionStringBuilder(connectionString);
            builder.InitialCatalog += $"_{suffix}";

            return builder.ConnectionString;
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

                SetSectionPath("ConnectionStrings:Main", _applicationConnectionString);
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}
