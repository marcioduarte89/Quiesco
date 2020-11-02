namespace Products.API.IntegrationTests.Builders
{
    using Infrastructure;
    using Models.Output;
    using Newtonsoft.Json;
    using Products.API.Models.Input.Property.Create;
    using SharedKernel.Tests.Serialization;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Models.Input.Property.Update;

    public class PropertyBuilder : IntegrationTestBase
    {
        public async Task<(Property Property, HttpStatusCode StatusCode)> CreateProperty(CreateProperty property)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(property), Encoding.UTF8, "application/json"))
            {
                var response = await Client.PostAsync($"properties/", content);
                return (SerializationHelpers.GetEntityFromStream<Property>(await response.Content.ReadAsStreamAsync()), response.StatusCode);
            }
        }

        public async Task<(Property Property, HttpStatusCode StatusCode)> UpdateProperty(int propertyId, UpdateProperty property)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(property), Encoding.UTF8, "application/json"))
            {
                var response = await Client.PutAsync($"properties/{propertyId}/", content);
                return (SerializationHelpers.GetEntityFromStream<Property>(await response.Content.ReadAsStreamAsync()), response.StatusCode);
            }
        }
    }
}
