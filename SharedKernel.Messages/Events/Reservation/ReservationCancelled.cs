namespace SharedKernel.Messages.Events.Reservation
{
    using System;

    /// <summary>
    /// Reservation cancelled event
    /// </summary>
    public class ReservationCancelled
    {
        /// <summary>
        /// Internal Reservation Id
        /// </summary>
        public Guid ReservationId { get; set; }
    }
}
