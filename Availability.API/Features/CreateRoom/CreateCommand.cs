namespace Availability.API.Features.CreateRoom
{
    using System;
    using System.Collections.Generic;
    using MediatR;
    using Models.Output;
    using Price = Models.Input.Room.Common.Price;

    /// <summary>
    /// Create command
    /// </summary>
    public class CreateCommand : IRequest<Room> {

        /// <summary>
        /// Property Id where the Room belongs to
        /// </summary>
        public int PropertyId { get; set; }

        /// <summary>
        /// Room id
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
