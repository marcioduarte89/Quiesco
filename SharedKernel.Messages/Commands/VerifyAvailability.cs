namespace SharedKernel.Messages.Commands
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Command used to verify availability
    /// </summary>
    public class VerifyAvailability
    {
        /// <summary>
        /// Internal Reservation Id
        /// </summary>
        public Guid ReservationId { get; set; }

        /// <summary>
        /// Property Id
        /// </summary>
        public int PropertyId { get; set; }

        /// <summary>
        /// Room Id
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Reservation Check-in
        /// </summary>
        public DateTime CheckIn { get; set; }

        /// <summary>
        /// Reservation Check-out
        /// </summary>
        public DateTime CheckOut { get; set; }
    }
}
