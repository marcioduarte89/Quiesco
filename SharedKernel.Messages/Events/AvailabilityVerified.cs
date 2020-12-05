namespace SharedKernel.Messages.Events
{
    using System;

    public class AvailabilityVerified
    {        
        /// <summary>
        /// Internal Reservation Id
        /// </summary>
        public Guid ReservationId { get; set; }

        /// <summary>
        /// Whether the room has availability
        /// </summary>
        public bool HasAvailability { get; set; }
    }
}
