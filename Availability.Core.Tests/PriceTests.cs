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
            Assert.Throws<AvailabilityException>(() => new Price(int.Parse(DateTime.Now.ToString("ddMMyyyy")), value));
        }

        [Test]
        public void Price_InvalidDateRangesToLessThanFrom_ThrowsAvailabilityException()
        {
            Assert.Throws<AvailabilityException>(() => new Price(int.Parse(DateTime.Now.Date.ToString("ddMMyyyy")), 1));
        }

        [Test]
        public void Price_ValidDate_CreatesPrice()
        {
            var date = int.Parse(DateTime.Now.AddDays(1).ToString("ddMMyyyy"));
            var price = new Price(date, 1);
            Assert.IsNotNull(price);
            Assert.AreEqual(1, price.Value);
        }

        [Test]
        public void SetPriceValue_ValidValue_UpdatesPrice()
        {
            var date = int.Parse(DateTime.Now.AddDays(1).ToString("ddMMyyyy"));
            var price = new Price(date, 1);
            price.SetPriceValue(2);
            Assert.IsNotNull(price);
            Assert.AreEqual(2, price.Value);
        }

        [Test]
        public void Price_EqualPricesBasedOnDate_ReturnsTrue()
        {
            var date = int.Parse(DateTime.Now.AddDays(1).ToString("ddMMyyyy"));
            var price = new Price(date, 1);
            var otherPrice = new Price(date, 1);

            Assert.IsTrue(price.Equals(otherPrice));
        }

        [Test]
        public void Price_NotEqualPrices_ReturnsTrue()
        {
            var date = int.Parse(DateTime.Now.AddDays(1).ToString("ddMMyyyy"));
            var dateTwo = int.Parse(DateTime.Now.AddDays(2).ToString("ddMMyyyy"));
            var price = new Price(date, 1);
            var otherPrice = new Price(dateTwo, 1);

            Assert.IsFalse(price.Equals(otherPrice));
        }
    }
}