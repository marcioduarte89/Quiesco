namespace SharedKernel.Messages.Events.Reservation
{
    using System;

    /// <summary>
    /// Event which notifies the booking was added
    /// </summary>
    public class BookingAdded
    {
        /// <summary>
        /// Internal Reservation Id
        /// </summary>
        public Guid ReservationId { get; set; }
    }
}
