namespace Products.API.Tests.Models.Property
{
    using API.Models.Input.Property.Update;
    using FluentValidation.TestHelper;
    using NUnit.Framework;

    public class UpdatePropertyTests
    {
        private UpdatePropertyValidator _validator;

        [SetUp]
        public void BeforeEach()
        {
            _validator = new UpdatePropertyValidator();
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Should_HaveError_WhenPropertyNameIsInvalid(string propertyName)
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Name, propertyName);
        }

        [Test]
        [TestCase("Some name")]
        public void Should_HaveNotHaveError_WhenPropertyNameIsValid(string propertyName)
        {
            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, propertyName);
        }
    }
}
