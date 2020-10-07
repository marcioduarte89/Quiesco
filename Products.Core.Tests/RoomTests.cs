namespace Products.Core.Tests
{
    using Common.Exceptions;
    using Enums;
    using Models;
    using NUnit.Framework;

    public class RoomTests
    {
        private Room _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Room(RoomTypes.KingSizeBed, 2);
            _sut.GetType().GetProperty("Id").SetValue(_sut, 1);;
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Room_InvalidNrOfOccupants_ThrowsProductException(int nrOfOccupants)
        {
            Assert.Throws<ProductException>(() => new Room(RoomTypes.KingSizeBed, nrOfOccupants));
        }

        [Test]
        public void Room_ValidDetails_CreatesTheRoom()
        {
            _sut = new Room(RoomTypes.KingSizeBed, 1);
            Assert.IsNotNull(_sut);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void UpdateRoom_InvalidNrOfOccupants_ThrowsProductException(int nrOfOccupants)
        {
            _sut = new Room(RoomTypes.KingSizeBed, 1);
            Assert.Throws<ProductException>(() => _sut.UpdateRoom(RoomTypes.SingleBed, nrOfOccupants));
        }

        [Test]
        public void UpdateRoom_ValidDetails_CreatesRoom()
        {
            _sut.UpdateRoom(RoomTypes.SingleBed, 1);
            Assert.IsNotNull(_sut);
            Assert.AreEqual(RoomTypes.SingleBed, _sut.AccommodationType);
            Assert.AreEqual(1, _sut.NrOfOccupants);
        }
    }
}
