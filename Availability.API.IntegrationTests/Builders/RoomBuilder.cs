namespace Availability.API.IntegrationTests.Builders
{
    using Infrastructure;
    using Models.Input.Room.Create;
    using Models.Output;
    using Newtonsoft.Json;
    using SharedKernel.Tests.Serialization;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class RoomBuilder : IntegrationTestBase
    {
        public async Task<(Room Room, HttpStatusCode StatusCode)> CreateRoom(int propertyId, CreateRoom room)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(room), Encoding.UTF8, "application/json"))
            {
                var response = await Client.PostAsync($"properties/{propertyId}/room", content);
                return (SerializationHelpers.GetEntityFromStream<Room>(await response.Content.ReadAsStreamAsync()), response.StatusCode);
            }
        }

        public async Task<(Room Room, HttpStatusCode StatusCode)> ChangePrices(int propertyId, int roomId, IEnumerable<Models.Input.Room.Common.Price> prices)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(prices), Encoding.UTF8, "application/json"))
            {
                var response = await Client.PutAsync($"properties/{propertyId}/room/{roomId}/prices", content);
                return (SerializationHelpers.GetEntityFromStream<Room>(await response.Content.ReadAsStreamAsync()), response.StatusCode);
            }
        }
    }
}
