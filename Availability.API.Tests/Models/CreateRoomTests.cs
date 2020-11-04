namespace Availability.API.Tests.Models
{
    using API.Models.Input.Room.Create;
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using System;

    public class CreateRoomTests
    {
        private CreateRoomValidator _validator;

        [SetUp]
        public void BeforeEach()
        {
            _validator = new CreateRoomValidator();
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Should_HaveError_WhenRoomIdHasInvalidValue(int roomId)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.RoomId, roomId);
        }

        [Test]
        [TestCase(10)]
        public void Should_HaveNotHaveError_WhenRoomIdHasValidValue(int roomId)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.RoomId, roomId);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Should_HaveError_WhenDefaultPricedHasInvalidValue(int defaultPrice)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.DefaultPrice, defaultPrice);
        }

        [Test]
        [TestCase(10)]
        public void Should_HaveNotHaveError_WhenDefaultValueHasValidValue(int roomId)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.RoomId, roomId);
        }

        [Test]
        public void Should_HaveHaveError_WhenBookedSlotsHaveInvalidDates()
        {
            var bookedSlots = new[] { DateTime.Now.AddDays(-1) };
            _validator.ShouldHaveValidationErrorFor(x => x.BookedSlots, bookedSlots);
        }

        [Test]
        public void Should_HaveNotHaveError_WhenBookedSlotsIsNull()
        {
            DateTime[] bookedSlots = null;
            _validator.ShouldNotHaveValidationErrorFor(x => x.BookedSlots, bookedSlots);
        }

        [Test]
        public void Should_HaveNotHaveError_WhenBookedSlotsIsEmpty()
        {
            var bookedSlots = new DateTime[]{};
            _validator.ShouldNotHaveValidationErrorFor(x => x.BookedSlots, bookedSlots);
        }
    }
}
