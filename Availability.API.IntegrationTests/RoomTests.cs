namespace Availability.API.IntegrationTests
{
    using System.Collections.Generic;
    using Builders;
    using Builders.Fakers;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Models.Input.Room.Common;

    [TestFixture]
    public class RoomTests : RoomBuilder
    {
        [Test]
        public async Task CreateRoom_WhenProvidingAllValidDetails_ShouldCreateRoom()
        {
            var room = CreateRoomFaker.Generate(true, true);
            
            var response = await CreateRoom(1, room);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Room);
            Assert.AreEqual(response.Room.RoomId, room.RoomId);
            Assert.AreEqual(response.Room.PropertyId, 1);
            Assert.NotNull(response.Room.BookedSlots);
            Assert.AreEqual(1, response.Room.BookedSlots.Count());
            Assert.NotNull(response.Room.Prices);
            Assert.AreEqual(1, response.Room.Prices.Count());
        }

        [Test]
        public async Task CreateRoom_WhenProvidingOnlyRequiredFieldsDetails_ShouldCreateRoom()
        {
            var room = CreateRoomFaker.Generate(false, false);

            var response = await CreateRoom(1, room);

            Assert.IsNotNull(response.Room);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(response.Room.RoomId, response.Room.RoomId);
            Assert.AreEqual(response.Room.PropertyId, 1);
            Assert.IsEmpty(response.Room.BookedSlots);
            Assert.IsEmpty(response.Room.Prices);
        }

        [Test]
        public async Task CreateRoom_WhenProvidingNonExistingRoom_ReturnsBadRequest()
        {
            var room = CreateRoomFaker.Generate(true, true);
            await CreateRoom(1, room);

            using (var content = new StringContent(JsonConvert.SerializeObject(room), Encoding.UTF8, "application/json"))
            {
                var newResponse = await Client.PostAsync($"properties/1/room", content);
                Assert.AreEqual(HttpStatusCode.BadRequest, newResponse.StatusCode);
            }
        }

        [Test]
        public async Task ChangePrice_WhenProvidingNonExistingRoom_ReturnsNotFound()
        {
            var price = PricesFaker.Generate();
            await CreateRoom(false, false);
            var response = await ChangePrices(1111111, 1111111, new List<Price> { price });

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task ChangePrice_WhenChangingPriceForBookedSlots_ReturnsBadRequest()
        {
            var roomResponse = await CreateRoom(true, true);
            var price = PricesFaker.Generate();
            price.Date = roomResponse.Room.BookedSlots[0];
            var response = await ChangePrices(roomResponse.Room.PropertyId, roomResponse.Room.RoomId, new List<Price> { price });

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task ChangePrice_WhenUpdatingOneAndAddingOnePrice_ShouldCreateRoom()
        {
            var roomResponse = await CreateRoom(true, true);
            var price = PricesFaker.Generate();
            price.Date = roomResponse.Room.Prices.FirstOrDefault().Date;
            price.Value = 1500;
            var otherPrice = PricesFaker.Generate();
            var response = await ChangePrices(roomResponse.Room.PropertyId, roomResponse.Room.RoomId, new List<Price> { price, otherPrice });

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Room);
            Assert.AreEqual(response.Room.RoomId, response.Room.RoomId);
            Assert.AreEqual(response.Room.PropertyId, 1);
            Assert.IsNotEmpty(response.Room.BookedSlots);
            Assert.IsNotEmpty(response.Room.Prices);
            Assert.AreEqual(2, response.Room.Prices.Count());
            Assert.IsTrue(response.Room.Prices.Any(x => x.Date == price.Date && x.Value == 1500));
        }
    }
}
