namespace Products.API.IntegrationTests
{
    using Microsoft.Extensions.Configuration;
    using NUnit.Framework;
    using SharedKernel.Tests.Deployment;
    using System;
    using System.Net.Http;

    [SetUpFixture]
    public class IntegrationTestSetup
    {
        private string _applicationConnectionString;
        private IConfiguration _config;
        private Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<Startup> _factory;
        private string isTestingEnv = Environment.GetEnvironmentVariable("CI");

        protected internal static HttpClient Client;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            _config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .AddJsonFile("appsettings.Development.json")
               .Build();

            _applicationConnectionString = DatabaseDeployment.GetConnectionString(_config.GetConnectionString("Main"), "Products.Tests");

            if (!bool.TryParse(isTestingEnv, out var result) || !result)
            {
                DatabaseDeployment.UpdateConfig("ConnectionStrings:Main", _applicationConnectionString);
            }

            DatabaseDeployment.DeployDb(_applicationConnectionString, "Products.DB.dacpac");

            _factory = new IntegrationTestsApplicationFactory<Startup>();
            Client = _factory.CreateClient();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            _factory?.Dispose();
            Client?.Dispose();
            DatabaseDeployment.DropDb(_applicationConnectionString);
        }
    }
}
