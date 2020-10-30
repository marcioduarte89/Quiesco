namespace Products.API.IntegrationTests
{
    using System.Net.Http;

    public class IntegrationTestBase
    {
        protected HttpClient Client = IntegrationTestSetup.Client;
    }
}
