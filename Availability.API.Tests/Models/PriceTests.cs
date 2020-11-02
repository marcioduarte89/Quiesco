namespace Availability.API.Tests.Models
{
    using API.Models.Input.Room.Common;
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using System;

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
        public void Should_HaveError_WhenDatedHasInvalidValue()
        {
            var date = DateTime.Now.AddDays(-1);
            _validator.ShouldHaveValidationErrorFor(x => x.Date, date);
        }

        [Test]
        public void Should_HaveNotHaveError_WhenDateHasValidValue()
        {
            var date = DateTime.Now.AddDays(1);
            _validator.ShouldNotHaveValidationErrorFor(x => x.Date, date);
        }
    }
}
