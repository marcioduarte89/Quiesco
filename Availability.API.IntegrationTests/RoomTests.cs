namespace Availability.API.IntegrationTests
{
    using AutoFixture;
    using AutoFixture.NUnit3;
    using Availability.API.Models.Input.Room.Create;
    using Availability.Common.Extensions;
    using Builders;
    using Models.Input.Room.Common;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    [TestFixture]
    [Category("Integration")]
    public class RoomTests : RoomBuilder
    {
        [Test, AutoData]
        public async Task CreateRoom_WhenProvidingAllValidDetails_ShouldCreateRoom(CreateRoom room)
        {
            room.RoomId = 1;
            room.BookedSlots = new[] { DateTime.Now.AddYears(1) };
            room.Prices = new Price[]
            {
                new Price()
                {
                    Date =  DateTime.Now.AddDays(1),
                    Value = 10
                }
            };

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

        [Test, AutoData]
        public async Task CreateRoom_WhenProvidingOnlyRequiredFieldsDetails_ShouldCreateRoom(CreateRoom room)
        {
            room.RoomId = 2;
            room.BookedSlots = null;
            room.Prices = null;

            var response = await CreateRoom(1, room);

            Assert.IsNotNull(response.Room);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(response.Room.RoomId, response.Room.RoomId);
            Assert.AreEqual(response.Room.PropertyId, 1);
            Assert.IsEmpty(response.Room.BookedSlots);
            Assert.IsEmpty(response.Room.Prices);
        }

        [Test, AutoData]
        public async Task CreateRoom_WhenProvidingExistingRoom_ReturnsBadRequest(CreateRoom room)
        {
            await CreateRoom(1, room);
            var responseNew = await CreateRoom(1, room);

            Assert.AreEqual(HttpStatusCode.BadRequest, responseNew.StatusCode);
        }

        [Test, AutoData]
        public async Task CreateRoom_WhenCreatingARoomWithSameIdUnderDifferentProperty_CreatesRoom(CreateRoom room)
        {
            room.RoomId = 2;
            room.BookedSlots = new[] { DateTime.Now.AddYears(1) };
            room.Prices = null;

            var response = await CreateRoom(2, room);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Room);
        }

        [Test, AutoData]
        public async Task ChangePrice_WhenProvidingNonExistingRoom_ReturnsNotFound(Price price)
        {
            price.Date =  DateTime.Now.AddDays(1) ;
            var response = await ChangePrices(1111111, 1111111, new List<Price> { price });

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test, AutoData]
        public async Task ChangePrice_WhenChangingPriceForBookedSlots_ReturnsBadRequest(CreateRoom room, Price price)
        {
            room.RoomId = 3;
            room.BookedSlots = new[] { DateTime.Now.AddYears(1) };
            room.Prices = null;
            var roomResponse = await CreateRoom(1, room);

            price.Date = roomResponse.Room.BookedSlots[0];
            var response = await ChangePrices(roomResponse.Room.PropertyId, roomResponse.Room.RoomId, new List<Price> { price });

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test, AutoData]
        public async Task ChangePrice_WhenUpdatingOneAndAddingOnePrice_ShouldCreateRoom(CreateRoom room, Price price)
        {
            room.RoomId = 4;
            room.BookedSlots = new[] { DateTime.Now.AddYears(1) };
            room.Prices = new Price[]
            {
                new Price()
                {
                    Date =  DateTime.Now.AddDays(1),
                    Value = 10
                }
            };

            var roomResponse = await CreateRoom(1, room);

            price.Date = roomResponse.Room.Prices.FirstOrDefault().Date;
            price.Value = 1500;
            var otherPrice = new Fixture().Create<Price>();
            otherPrice.Date = DateTime.Now.AddDays(2);
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
