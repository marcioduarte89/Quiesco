namespace SharedKernel.Messages.Events.Reservation
{
    using System;

    /// <summary>
    /// Event which checks property availability
    /// </summary>
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
