namespace SharedKernel.Messages.Commands.Reservation
{
    using System;

    /// <summary>
    /// Command which completes the reservation
    /// </summary>
    public class CompleteReservation
    {
        /// <summary>
        /// Internal Reservation Id
        /// </summary>
        public Guid ReservationId { get; set; }
    }
}
