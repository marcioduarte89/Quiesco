namespace Availability.Core.Tests {
    using System;
    using Common.Exceptions;
    using Models;
    using NUnit.Framework;

    public class PriceTests {

        private Price _sut;

        [SetUp]
        public void Setup() {
        }

        [Test]
        [TestCase(-1)]
        public void Price_InvalidValue_ThrowsAvailabilityException(decimal value)
        {
            Assert.Throws<AvailabilityException>(() => Price.Create(DateTime.Now, value));
        }

        [Test]
        public void Price_InvalidDateRangesToLessThanFrom_ThrowsAvailabilityException()
        {
            Assert.Throws<AvailabilityException>(() => Price.Create(DateTime.Now.Date, 1));
        }

        [Test]
        public void Price_ValidDate_CreatesPrice()
        {
            var date = DateTime.Now.AddDays(1);
            var price = Price.Create(date, 1);
            Assert.IsNotNull(price);
            Assert.AreEqual(1, price.Value);
        }

        [Test]
        public void SetPriceValue_ValidValue_UpdatesPrice()
        {
            var date = DateTime.Now.AddDays(1);
            var price = Price.Create(date, 1);
            price.SetPriceValue(2);
            Assert.IsNotNull(price);
            Assert.AreEqual(2, price.Value);
        }

        [Test]
        public void Price_EqualPricesBasedOnDate_ReturnsTrue()
        {
            var date = DateTime.Now.AddDays(1);
            var price = Price.Create(date, 1);
            var otherPrice = Price.Create(date, 1);

            Assert.IsTrue(price.Equals(otherPrice));
        }

        [Test]
        public void Price_NotEqualPrices_ReturnsTrue()
        {
            var date = DateTime.Now.AddDays(1);
            var dateTwo = DateTime.Now.AddDays(2);
            var price = Price.Create(date, 1);
            var otherPrice = Price.Create(dateTwo, 1);

            Assert.IsFalse(price.Equals(otherPrice));
        }
    }
}