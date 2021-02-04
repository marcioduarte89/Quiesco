namespace SharedKernel.Messages.Events.Reservation
{
    using System;

    /// <summary>
    /// Failed to add booking event
    /// </summary>
    public class AddBookingFailed
    {
        /// <summary>
        /// Internal Reservation Id
        /// </summary>
        public Guid ReservationId { get; set; }

        /// <summary>
        /// Reason for Add booking failure
        /// </summary>
        public string Reason { get; set; }
    }
}
