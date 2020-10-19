namespace Availability.API.Models.Output
{
    using System.Collections.Generic;

    /// <summary>
    /// Room Output model
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Room Id
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Property Id where the Room belongs to
        /// </summary>
        public int PropertyId { get; set; }

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
        public int[] BookedSlots { get; set; }
    }
}
