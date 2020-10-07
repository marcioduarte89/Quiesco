namespace Products.Core.Tests
{
    using Common.Exceptions;
    using Enums;
    using Models;
    using NUnit.Framework;
    using System.Linq;

    public class PropertyTests
    {
        private Property _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Property(PropertyTypes.Hotel, "Hilton");
            var room = new Room(RoomTypes.KingSizeBed, 2);
            room.GetType().GetProperty("Id").SetValue(room, 1);
            _sut.AddRoom(room);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        public void Property_InvalidName_ThrowsProductException(string name)
        {
            Assert.Throws<ProductException>(() => new Property(PropertyTypes.Hotel, name));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        public void SetName_InvalidName_ThrowsProductException(string name)
        {
            Assert.Throws<ProductException>(() => _sut.SetName(name));
        }

        [Test]
        public void SetName_ValidName_ChangesName()
        {
            _sut.SetName("Cesar's Palace");
            Assert.AreEqual("Cesar's Palace", _sut.Name);
        }

        [Test]
        public void AddRoom_InvalidRoom_ThrowsProductException()
        {
            Assert.Throws<ProductException>(() => _sut.AddRoom(null));
        }


        [Test]
        public void AddRoom_ValidRoom_AddsRoom()
        {
            _sut.AddRoom(new Room(RoomTypes.KingSizeBed, 1));
            Assert.AreEqual(2, _sut.Rooms.Count());
        }

        [Test]
        public void GetRoom_RoomsNotFound_GetsTheRoom()
        {
            var room = _sut.GetRoom(1);
            Assert.IsNotNull(room);
        }
    }
}