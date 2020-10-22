namespace Availability.API.Tests.Models
{
    using API.Models.Input.Room.Common;
    using FluentValidation.TestHelper;
    using NUnit.Framework;

    public class PriceTests
    {
        private PriceValidator _validator;

        [SetUp]
        public void BeforeEach()
        {
            _validator = new PriceValidator();
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Should_HaveError_WhenValueHasInvalidValue(int value)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Value, value);
        }

        [Test]
        [TestCase(10)]
        public void Should_HaveNotHaveError_WhenValueHasValidValue(int value)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Value, value);
        }

        [Test]
        [TestCase(0)]
        [TestCase(101020000)]
        [TestCase(20201510)]
        public void Should_HaveError_WhenDatedHasInvalidValue(int date)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Date, date);
        }

        [Test]
        [TestCase(10102020)]
        public void Should_HaveNotHaveError_WhenDateHasValidValue(int value)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Date, value);
        }
    }
}
