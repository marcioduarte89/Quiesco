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
            Assert.Throws<AvailabilityException>(() => new Price(int.Parse(DateTime.Now.ToString("ddmmyyyy")), int.Parse(DateTime.Now.AddDays(1).ToString("ddMMyyyy")), value));
        }

        [Test]
        public void Price_InvalidDateRangesSameDate_ThrowsAvailabilityException()
        {
            var sameDate = int.Parse(DateTime.Now.ToString("ddMMyyyy"));
            Assert.Throws<AvailabilityException>(() => new Price(sameDate, sameDate, 1));
        }

        [Test]
        public void Price_InvalidDateRangesToLessThanFrom_ThrowsAvailabilityException()
        {
            Assert.Throws<AvailabilityException>(() => new Price(int.Parse(DateTime.Now.Date.ToString("ddMMyyyy")), int.Parse(DateTime.Now.AddDays(-1).ToString("ddMMyyyy")), 1));
        }

        [Test]
        public void Price_ValidDateRanges_CreatesPrice()
        {
            var fromDate = int.Parse(DateTime.Now.Date.ToString("ddMMyyyy"));
            var toDate = int.Parse(DateTime.Now.AddDays(1).ToString("ddMMyyyy"));
            var price = new Price(fromDate, toDate, 1);
            Assert.IsNotNull(price);
            Assert.AreEqual(1, price.Value);
            Assert.AreEqual(fromDate, price.FromDate);
            Assert.AreEqual(toDate, price.ToDate);
        }
    }
}