namespace Products.API.IntegrationTests.Infrastructure
{
    using System.Net.Http;

    public class IntegrationTestBase
    {
        protected HttpClient Client = IntegrationTestSetup.Client;
    }
}
