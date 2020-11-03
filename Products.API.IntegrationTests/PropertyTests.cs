namespace Products.API.IntegrationTests
{
    using AutoFixture.NUnit3;
    using Builders;
    using NUnit.Framework;
    using Products.API.Models.Input.Property.Create;
    using Products.API.Models.Input.Property.Update;
    using Products.API.Models.Input.Room.Create;
    using Products.API.Models.Input.Room.Update;
    using System.Net;
    using System.Threading.Tasks;

    [TestFixture]
    [Category("Integration")]
    public class PropertyTests : PropertyBuilder
    {
        [Test, AutoData]
        public async Task CreateProperty_WhenProvidingAllValidDetails_ShouldCreateProperty(CreateProperty property)
        {
            var response = await CreateProperty(property);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.Property);
            Assert.AreEqual(property.Name, response.Property.Name);
            Assert.AreEqual(property.Type, response.Property.Type);
        }

        [Test, AutoData]
        public async Task UpdateProperty_WhenUpdatingName_ShouldUpdateProperty(CreateProperty property)
        {
            var createPropertyResponse = await CreateProperty(property);

            var updateProperty = new UpdateProperty() { Name = "Some updated name" };

            var response = await UpdateProperty(createPropertyResponse.Property.Id, updateProperty);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Property);
            Assert.AreEqual(updateProperty.Name, response.Property.Name);
            Assert.AreEqual(createPropertyResponse.Property.Type, response.Property.Type);
        }

        [Test]
        public async Task UpdateProperty_WhenNonExistingProperty_ReturnsNotFound()
        {
            var updateProperty = new UpdateProperty() { Name = "Some updated name" };

            var response = await UpdateProperty(1111, updateProperty);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test, AutoData]
        public async Task CreateRoom_WhenNonExistingProperty_ReturnsNotFound(CreateRoom room)
        {
            var response = await CreateRoom(11111, room);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test, AutoData]
        public async Task CreateRoom_WhenExistingProperty_ShoudCreateRoom(CreateProperty property, CreateRoom room)
        {
            var propertyResponse = await CreateProperty(property);
            var response = await CreateRoom(propertyResponse.Property.Id, room);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(room.NrOfOccupants, response.Room.NrOfOccupants);
            Assert.AreEqual(room.AccommodationType, response.Room.AccommodationType);
        }

        [Test, AutoData]
        public async Task UpdateRoom_WhenNonExistingProperty_ReturnsNotFound(UpdateRoom room)
        {
            var response = await UpdateRoom(111111, 1, room);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test, AutoData]
        public async Task UpdateRoom_WhenNonExistingRoom_ReturnsNotFound(CreateProperty property, UpdateRoom room)
        {
            var propertyResponse = await CreateProperty(property);
            var response = await UpdateRoom(propertyResponse.Property.Id, 111111, room);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);;
        }

        [Test, AutoData]
        public async Task UpdateRoom_WhenValidDetails_ShouldUpdateRoom(CreateProperty property, CreateRoom room, UpdateRoom updateRoom)
        {
            var propertyResponse = await CreateProperty(property);
            var response = await CreateRoom(propertyResponse.Property.Id, room);

            updateRoom.AccommodationType = response.Room.AccommodationType;
            updateRoom.NrOfOccupants = response.Room.NrOfOccupants;

            var updateRoomResponse = await UpdateRoom(propertyResponse.Property.Id, response.Room.Id, updateRoom);

            Assert.AreEqual(HttpStatusCode.OK, updateRoomResponse.StatusCode);
            Assert.AreEqual(updateRoom.NrOfOccupants, updateRoomResponse.Room.NrOfOccupants);
            Assert.AreEqual(updateRoom.AccommodationType, updateRoomResponse.Room.AccommodationType);
        }
    }
}
