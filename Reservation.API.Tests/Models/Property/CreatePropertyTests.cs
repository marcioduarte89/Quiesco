//namespace Reservation.API.Tests.Models.Property {
//    using API.Models.Input.Property.Create;
//    using Core.Enums;
//    using FluentValidation.TestHelper;
//    using NUnit.Framework;

//    public class CreatePropertyTests {

//        private CreatePropertyValidator _validator;

//        [SetUp]
//        public void BeforeEach() {
//            _validator = new CreatePropertyValidator();
//        }

//        [Test]
//        [TestCase("")]
//        [TestCase(" ")]
//        [TestCase(null)]
//        public void Should_HaveError_WhenPropertyNameIsInvalid(string propertyName) {
//            _validator.ShouldHaveValidationErrorFor(x => x.Name, propertyName);
//        }

//        [Test]
//        [TestCase("Some name")]
//        public void Should_HaveNotHaveError_WhenPropertyNameIsValid(string propertyName) {
//            _validator.ShouldNotHaveValidationErrorFor(x => x.Name, propertyName);
//        }

//        [Test]
//        [TestCase(-1)]
//        [TestCase(0)]
//        [TestCase(3)]
//        public void Should_HaveError_WhenPropertyTypeHasInvalidValue(int propertyType) {
//            _validator.ShouldHaveValidationErrorFor(x => x.Type, (PropertyTypes)propertyType);
//        }

//        [Test]
//        [TestCase(1)]
//        [TestCase(2)]
//        public void Should_HaveNotHaveError_henPropertyTypeHasValidValue(int propertyType) {
//            _validator.ShouldNotHaveValidationErrorFor(x => x.Type, (PropertyTypes)propertyType);
//        }
//    }
//}
