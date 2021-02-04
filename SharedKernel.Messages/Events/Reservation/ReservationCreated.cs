namespace SharedKernel.Messages.Events.Reservation
{
    using System;

    /// <summary>
    /// Reservation created
    /// </summary>
    public class ReservationCreated
    {
         /// <summary>
        /// Internal Reservation Id
        /// </summary>
        public Guid ReservationId { get; set; }
    }
}
