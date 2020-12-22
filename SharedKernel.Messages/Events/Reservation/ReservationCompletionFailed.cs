namespace SharedKernel.Messages.Events.Reservation
{
    using System;

    /// <summary>
    /// Reservation completed failed event
    /// </summary>
    public class ReservationCompletionFailed
    {
        /// <summary>
        /// Internal Reservation Id
        /// </summary>
        public Guid ReservationId { get; set; }

        /// <summary>
        /// Reason for which the reservation failed to complete
        /// </summary>
        public string Reason { get; set; }
    }
}
