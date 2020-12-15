namespace Reservation.API.Tests.Models.Property
{
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using Reservations.API.Models.Input.Common;

    public class UserTests
    {
        private UserValidator _validator;

        [SetUp]
        public void BeforeEach()
        {
            _validator = new UserValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Should_HaveError_WhenUserEmailIsInvalid(string userEmail)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.UserEmail, userEmail);
        }

        [Test]
        [TestCase("Some email")]
        public void Should_HaveNotHaveError_WhenUserEmailIsValid(string userEmail)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.UserEmail, userEmail);
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Should_HaveError_WhenNameIsInvalid(string name)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [Test]
        [TestCase("Some name")]
        public void Should_HaveNotHaveError_WhenNameIsValid(string name)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, name);
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Should_HaveError_WhenLastNameIsInvalid(string lastName)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.LastName, lastName);
        }

        [Test]
        [TestCase("Some name")]
        public void Should_HaveNotHaveError_WhenLastNameIsValid(string lastName)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.LastName, lastName);
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Should_HaveError_WhenPhoneNumberIsInvalid(string phoneNumber)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.PhoneNumber, phoneNumber);
        }

        [Test]
        [TestCase("99999")]
        public void Should_HaveNotHaveError_WhenPhoneNumberIsValid(string phoneNumber)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber, phoneNumber);
        }
    }
}
