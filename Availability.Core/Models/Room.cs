﻿namespace Availability.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Exceptions;
    using Common.Extensions;

    /// <summary>
    /// Room domain entity
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Booked slots
        /// </summary>
        private List<int> _bookedSlots;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="propertyId"></param>
        /// <param name="roomId"></param>
        /// <param name="defaultPrice">room default price</param>
        public Room(int propertyId, int roomId, decimal defaultPrice)
        {
            DefaultPrice = defaultPrice;
            PropertyId = propertyId;
            RoomId = roomId;
            _bookedSlots = new List<int>();
            Prices = new HashSet<Price>();
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
        public IEnumerable<int> BookedSlots { get { return _bookedSlots; } }

        /// <summary>
        /// Adds room booking
        /// </summary>
        /// <param name="bookedSlots">Booked slots</param>
        public void AddBookings(int[] bookedSlots)
        {
            // Check for any duplicates
            var hashSet = new HashSet<int>(bookedSlots);
            if (hashSet.Count != bookedSlots.Length)
            {
                throw new AvailabilityException(AvailabilityException.INVALID_DATA, $"Bookings cannot contain duplicates");
            }

            foreach (var bookedSlot in bookedSlots)
            {
                // Check if there are any bookings for past dates
                var bookedSlotAsDate = DateExtensions.Convert(bookedSlot);
                if (bookedSlotAsDate.CompareTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)) <= 0)
                {
                    throw new AvailabilityException(AvailabilityException.INVALID_DATA, $"Cannot create a booking for a past date");
                }

                // Trying to create a booking for an already existing booking
                if (_bookedSlots.Any(x => x == bookedSlot))
                {
                    throw new AvailabilityException(AvailabilityException.INVALID_DATA, $"Cannot have overlapping bookings or double bookings");
                }

                _bookedSlots.Add(bookedSlot);
            }
        }

        public void SetDatePrices(List<Price> prices)
        {
            if (prices == null || !prices.Any())
            {
                return;
            }

            if (prices.Any(x => _bookedSlots.Any(y => x.Date == y)))
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
            }
        }
    }
}