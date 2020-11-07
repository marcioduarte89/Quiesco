//namespace Reservations.IntegrationTests.Builders {
//    using Infrastructure;
//    using Models.Output;
//    using Newtonsoft.Json;
//    using Products.API.Models.Input.Property.Create;
//    using SharedKernel.Tests.Serialization;
//    using System.Net;
//    using System.Net.Http;
//    using System.Text;
//    using System.Threading.Tasks;
//    using Models.Input.Property.Update;
//    using Products.API.Models.Input.Room.Create;
//    using Products.API.Models.Input.Room.Update;

//    public class PropertyBuilder : IntegrationTestBase {
//        public async Task<(Property Property, HttpStatusCode StatusCode)> CreateProperty(CreateProperty property) {
//            using (var content = new StringContent(JsonConvert.SerializeObject(property), Encoding.UTF8, "application/json")) {
//                var response = await Client.PostAsync($"properties/", content);
//                return (SerializationHelpers.GetEntityFromStream<Property>(await response.Content.ReadAsStreamAsync()), response.StatusCode);
//            }
//        }

//        public async Task<(Property Property, HttpStatusCode StatusCode)> UpdateProperty(int propertyId, UpdateProperty property) {
//            using (var content = new StringContent(JsonConvert.SerializeObject(property), Encoding.UTF8, "application/json")) {
//                var response = await Client.PutAsync($"properties/{propertyId}/", content);
//                return (SerializationHelpers.GetEntityFromStream<Property>(await response.Content.ReadAsStreamAsync()), response.StatusCode);
//            }
//        }

//        public async Task<(Room Room, HttpStatusCode StatusCode)> CreateRoom(int propertyId, CreateRoom room) {
//            using (var content = new StringContent(JsonConvert.SerializeObject(room), Encoding.UTF8, "application/json")) {
//                var response = await Client.PostAsync($"properties/{propertyId}/rooms", content);
//                return (SerializationHelpers.GetEntityFromStream<Room>(await response.Content.ReadAsStreamAsync()), response.StatusCode);
//            }
//        }

//        public async Task<(Room Room, HttpStatusCode StatusCode)> UpdateRoom(int propertyId, int roomId, UpdateRoom room) {
//            using (var content = new StringContent(JsonConvert.SerializeObject(room), Encoding.UTF8, "application/json")) {
//                var response = await Client.PutAsync($"properties/{propertyId}/rooms/{roomId}", content);
//                return (SerializationHelpers.GetEntityFromStream<Room>(await response.Content.ReadAsStreamAsync()), response.StatusCode);
//            }
//        }
//    }
//}
