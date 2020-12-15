namespace Reservation.API.Tests.Models.Property
{
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using Reservation.API.Models.Input.Reservation.Create;
    using Reservations.API.Models.Input.Common;
    using System;

    public class CreateReservationTests
    {
        private CreateReservationValidator _validator;

        [SetUp]
        public void BeforeEach()
        {
            _validator = new CreateReservationValidator();
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Should_HaveError_WhenPropertyIdHasInvalidValue(int propertyId)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.PropertyId, propertyId);
        }

        [Test]
        [TestCase(10)]
        public void Should_HaveNotError_WhenPropertyIdHasValidValue(int propertyId)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.PropertyId, propertyId);
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
        public void Should_HaveNotError_WhenRoomIdHasValidValue(int roomId)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.RoomId, roomId);
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        public void Should_HaveNotError_WhenNrOfOccupantsHasValidValue(int nrOfOccupants)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.NumberOfOccupants, nrOfOccupants);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Should_HaveError_WhenNrOfOccupantsHasInvalidValue(int nrOfOccupants)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.NumberOfOccupants, nrOfOccupants);
        }

        [Test]
        public void Should_HaveError_WhenUserIsNull()
        {
            var user = default(User);
            _validator.ShouldHaveValidationErrorFor(x => x.User, user);
        }

        [Test]
        public void Should_NotHaveError_WhenUserIsProvided()
        {
            var reservation = new CreateReservation
            {
                CheckIn = DateTime.Now.AddDays(5),
                CheckOut = DateTime.Now,
                User = new User()
                {
                    Name = "User1",
                    LastName = "UserLastName",
                    UserEmail = "User1@gmail",
                    PhoneNumber = "99999",
                },
                NumberOfOccupants = 1,
                PropertyId = 1,
                RoomId = 1
            };

            _validator.ShouldNotHaveValidationErrorFor(x => x.User, reservation);
        }

        [Test]
        public void Should_HaveError_CheckInIsGreaterThanCheckOut()
        {
            var reservation = new CreateReservation
            {
                CheckIn = DateTime.Now.AddDays(5),
                CheckOut = DateTime.Now,
                User = new User(),
                NumberOfOccupants = 1,
                PropertyId = 1,
                RoomId = 1
            };

            _validator.ShouldHaveValidationErrorFor(c => c.CheckIn, reservation);
        }
    }
}
