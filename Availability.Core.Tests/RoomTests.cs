namespace Availability.Core.Tests
{
    using Common.Exceptions;
    using Models;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RoomTests
    {
        private Room _sut;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Create_InvalidPropertyId_ThrowsAvailabilityException(int propertyId)
        {
            Assert.Throws<AvailabilityException>(() => Room.Create(propertyId, 1, 1));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Create_InvalidRoomId_ThrowsAvailabilityException(int roomId)
        {
            Assert.Throws<AvailabilityException>(() => Room.Create(1, roomId, 1));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Create_InvalidDefaultPrice_ThrowsAvailabilityException(int defaultPrice)
        {
            Assert.Throws<AvailabilityException>(() => Room.Create(1, 1, defaultPrice));
        }

        [Test]
        public void AddBookings_DuplicatedBookings_ThrowsAvailabilityException()
        {
            var room = Room.Create(1, 1, 10);

            var duplicatedDateTime = DateTime.Now.AddDays(1);

            Assert.Throws<AvailabilityException>(() => room.AddBookings(new[] { duplicatedDateTime, duplicatedDateTime }));
        }

        [Test]
        public void AddBookings_BookingForAPastDate_ThrowsAvailabilityException()
        {
            _sut = Room.Create(1, 1, 10);

            var pastDateTime = DateTime.Now.AddDays(-1);

            Assert.Throws<AvailabilityException>(() => _sut.AddBookings(new[] { pastDateTime, DateTime.Now.AddDays(1) }));
        }

        [Test]
        public void AddBookings_AlreadyExistingBooking_ThrowsAvailabilityException()
        {
            _sut = Room.Create(1, 1, 10);
            _sut.AddBookings(new[]
            {
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2)
            });

            Assert.Throws<AvailabilityException>(() => _sut.AddBookings(new[]
            {
               DateTime.Now.Date.AddDays(1)
           }));
        }

        [Test]
        public void AddBookings_AddValidBooking_CreatesBookings()
        {
            _sut = Room.Create(1, 1, 10);
            _sut.AddBookings(new[]
            {
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2)
            });

            Assert.IsNotNull(_sut.BookedSlots);
            Assert.AreEqual(2, _sut.BookedSlots.Count());
        }

        [Test]
        public void SetDatePrices_ChangePriceWhenBookingAlreadyExistsInFromDate_ThrowsAvailabilityException()
        {

            _sut = Room.Create(1, 1, 10);
            _sut.AddBookings(new[] { DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(2) });

            Assert.Throws<AvailabilityException>(() => _sut.SetDatePrices(new List<Price>()
            {
                Price.Create(DateTime.Now.Date.AddDays(1), 10 ),
                Price.Create(DateTime.Now.Date.AddDays(4), 10 )
            }));
        }

        [Test]
        public void SetDatePrices_ValidPriceRange_SetsPrices()
        {
            _sut = Room.Create(1, 1, 10);
            _sut.SetDatePrices(new List<Price>()
            {
                Price.Create(DateTime.Now.Date.AddDays(1), 10),
                Price.Create(DateTime.Now.Date.AddDays(4), 10)
            });

            Assert.IsNotNull(_sut.Prices);
            Assert.AreEqual(2, _sut.Prices.Count());
        }

        [Test]
        public void SetDatePrices_OneNewPriceAndOneUpdated_SetsPrices()
        {
            _sut = Room.Create(1, 1, 10);
            _sut.SetDatePrices(new List<Price>()
            {
                Price.Create(DateTime.Now.Date.AddDays(1), 10)
            });

            _sut.SetDatePrices(new List<Price>()
            {
                Price.Create(DateTime.Now.Date.AddDays(1), 20),
                Price.Create(DateTime.Now.Date.AddDays(2), 30)
            });

            Assert.IsNotNull(_sut.Prices);
            Assert.AreEqual(2, _sut.Prices.Count());
            Assert.AreEqual(20, _sut.Prices.ElementAt(0).Value);
        }

        [Test]
        public void HasAvailability_PastCheckInDate_ThrowsAvailabilityException()
        {
            _sut = Room.Create(1, 1, 10);
            _sut.AddBookings(new[]
            {
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2)
            });

            Assert.Throws<AvailabilityException>(() => _sut.HasAvailability(DateTime.Now.Date.AddDays(-1), DateTime.Now.Date.AddDays(1)));
        }

        [Test]
        public void HasAvailability_PastCheckOutDate_ThrowsAvailabilityException()
        {
            _sut = Room.Create(1, 1, 10);
            _sut.AddBookings(new[]
            {
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2)
            });

            Assert.Throws<AvailabilityException>(() => _sut.HasAvailability(DateTime.Now.Date.AddDays(1), DateTime.Now.Date.AddDays(-1)));
        }

        [Test]
        public void HasAvailability_PastCheckInOverCheckoutDate_ThrowsAvailabilityException()
        {
            _sut = Room.Create(1, 1, 10);
            _sut.AddBookings(new[]
            {
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2)
            });

            Assert.Throws<AvailabilityException>(() => _sut.HasAvailability(DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(1)));
        }

        [Test]
        public void HasAvailability_NoIntersectingDates_HasAvailability()
        {
            _sut = Room.Create(1, 1, 10);
            _sut.AddBookings(new[]
            {
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2)
            });

            Assert.IsTrue(_sut.HasAvailability(DateTime.Now.Date.AddDays(3), DateTime.Now.Date.AddDays(4)));
        }

        [Test]
        public void HasAvailability_IntersectingDates_HasAvailability()
        {
            _sut = Room.Create(1, 1, 10);
            _sut.AddBookings(new[]
            {
                DateTime.Now.Date.AddDays(1),
                DateTime.Now.Date.AddDays(2)
            });

            Assert.IsFalse(_sut.HasAvailability(DateTime.Now.Date.AddDays(2), DateTime.Now.Date.AddDays(3)));
        }
    }
}
