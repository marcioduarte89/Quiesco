namespace Products.API.Tests.Models.Room
{
    using API.Models.Input.Room.Update;
    using Core.Enums;
    using FluentValidation.TestHelper;
    using NUnit.Framework;

    public class UpdateRoomTests
    {
        private UpdateRoomValidator _validator;

        [SetUp]
        public void BeforeEach()
        {
            _validator = new UpdateRoomValidator();
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Should_HaveError_WhenNrOfOccupantsHasInvalidValue(int nrOfOccupants)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.NrOfOccupants, nrOfOccupants);
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        public void Should_HaveNotHaveError_WhenNrOfOccupantsHasValidValue(int nrOfOccupants)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.NrOfOccupants, nrOfOccupants);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(4)]
        public void Should_HaveError_WhenAccommodationTypeTypeHasInvalidValue(int accommodationType)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.AccommodationType, (RoomTypes)accommodationType);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Should_HaveNotHaveError_WhenAccommodationTypeTypeHasValidValue(int accommodationType)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.AccommodationType, (RoomTypes)accommodationType);
        }
    }
}
