namespace Availability.API.Models.Input.Room.Create
{
    using System;
    using System.Collections.Generic;
    using Common;

    /// <summary>
    /// Creates availability for a new room
    /// </summary>
    public class CreateRoom
    {
        /// <summary>
        /// Room Id
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Default price used when no ranges are specified
        /// </summary>
        public decimal DefaultPrice { get; set; }

        /// <summary>
        /// Prices for time ranges
        /// </summary>
        public IEnumerable<Price> Prices { get; set; }

        /// <summary>
        /// Slots when the room is booked
        /// </summary>
        public DateTime[] BookedSlots { get; set; }
    }
}
