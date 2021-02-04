namespace SharedKernel.Messages.Events.Reservation
{
    using System;

    /// <summary>
    /// Reservation completed event
    /// </summary>
    public class ReservationCompleted
    {
        /// <summary>
        /// Internal Reservation Id
        /// </summary>
        public Guid ReservationId { get; set; }
    }
}
