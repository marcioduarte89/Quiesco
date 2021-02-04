namespace SharedKernel.Messages.Commands.Reservation
{
    using System;

    /// <summary>
    /// Command which cancels the reservation
    /// </summary>
    public class CancelReservation
    {
        /// <summary>
        /// Internal Reservation Id
        /// </summary>
        public Guid ReservationId { get; set; }

        /// <summary>
        /// Reason for reservation being cancelled
        /// </summary>
        public string Reason { get; set; }
    }
}
