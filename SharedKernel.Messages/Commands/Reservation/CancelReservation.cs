namespace SharedKernel.Messages.Commands.Reservation
{
    using SharedKernel.Messages.Common;
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
    }
}
