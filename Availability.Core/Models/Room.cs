namespace Availability.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Exceptions;
    using Common.Extensions;

    /// <summary>
    /// Room domain entity
    /// </summary>
    public class Room :  BaseEntity
    {
        /// <summary>
        /// Booked slots
        /// </summary>
        private List<DateTime> _bookedSlots;

        /// <summary>
        /// Constructor
        /// </summary>
        private Room()
        {
            _bookedSlots = new List<DateTime>();
            Prices = new HashSet<Price>();
        }

        /// <summary>
        /// Room creator
        /// </summary>
        /// <param name="propertyId"></param>
        /// <param name="roomId"></param>
        /// <param name="defaultPrice">room default price</param>
        public static Room Create(int propertyId, int roomId, decimal defaultPrice)
        {
            ValidateRoom(propertyId, roomId, defaultPrice);
            return new Room()
            {
                DefaultPrice = defaultPrice,
                PropertyId = propertyId,
                RoomId = roomId,
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
        }

        /// <summary>
        /// Room unique Id
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Room Id
        /// </summary>
        public int RoomId { get; private set; }

        /// <summary>
        /// Property Id where the Room belongs to
        /// </summary>
        public int PropertyId { get; private set; }

        /// <summary>
        /// Default price used when no ranges are specified
        /// </summary>
        public decimal DefaultPrice { get; private set; }

        /// <summary>
        /// Prices for time ranges
        /// </summary>
        public HashSet<Price> Prices { get; private set; }

        /// <summary>
        /// Slots when the room is booked
        /// </summary>
        public IEnumerable<DateTime> BookedSlots { get { return _bookedSlots; } }

        /// <summary>
        /// Adds room booking
        /// </summary>
        /// <param name="bookedSlots">Booked slots</param>
        public void AddBookings(DateTime[] bookedSlots)
        {
            // Check for any duplicates
            var hashSet = new HashSet<DateTime>(bookedSlots);
            if (hashSet.Count != bookedSlots.Length)
            {
                throw new AvailabilityException(AvailabilityException.INVALID_DATA, $"Bookings cannot contain duplicates");
            }

            foreach (var bookedSlot in bookedSlots)
            {
                // Check if there are any bookings for past dates
                if (bookedSlot.CompareTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)) <= 0)
                {
                    throw new AvailabilityException(AvailabilityException.INVALID_DATA, $"Cannot create a booking for a past date");
                }

                // Trying to create a booking for an already existing booking
                if (_bookedSlots.Any(x => x == bookedSlot))
                {
                    throw new AvailabilityException(AvailabilityException.INVALID_DATA, $"Cannot have overlapping bookings or double bookings");
                }

                _bookedSlots.Add(bookedSlot.Date);
                UpdatedDate = DateTime.Now; // need to refactor this - leaving it here for now..
            }
        }

        public void SetDatePrices(List<Price> prices)
        {
            if (prices == null || !prices.Any())
            {
                return;
            }

            // Check for any duplicates
            if (prices.Count != prices.Distinct().Count())
            {
                throw new AvailabilityException(AvailabilityException.INVALID_DATA, $"Prices cannot contain duplicates");
            }

            if (prices.Any(x => _bookedSlots.Any(y => x.Date.Date == y.Date)))
            {
                throw new AvailabilityException(AvailabilityException.INVALID_DATA, $"Cannot change the price for a time slot where a booking is already made");
            }

            foreach (var newPrice in prices)
            {
                if (Prices.TryGetValue(newPrice, out var price))
                {
                    price.SetPriceValue(newPrice.Value);
                }
                else
                {
                    Prices.Add(newPrice);
                }

                UpdatedDate = DateTime.Now; // need to refactor this - leaving it here for now..
            }
        }

        /// <summary>
        /// Validates room for creation
        /// </summary>
        /// <param name="propertyId">property id</param>
        /// <param name="roomId">room id</param>
        /// <param name="defaultPrice">default price</param>
        ///<exception cref="AvailabilityException">If either propertyId, roomId or default price are invalid</exception>
        private static void ValidateRoom(int propertyId, int roomId, decimal defaultPrice)
        {
            if (propertyId <= 0)
            {
                throw new AvailabilityException(AvailabilityException.INVALID_DATA, "Property id can't be <= 0");
            }

            if (roomId <= 0)
            {
                throw new AvailabilityException(AvailabilityException.INVALID_DATA, "Room id can't be <= 0");
            }

            if (defaultPrice <= 0)
            {
                throw new AvailabilityException(AvailabilityException.INVALID_DATA, "Default price id can't be <= 0");
            }
        }
    }
}
