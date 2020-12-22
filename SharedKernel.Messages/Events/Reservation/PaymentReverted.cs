namespace SharedKernel.Messages.Events.Reservation
{
    using System;

    /// <summary>
    /// Payment reverted event
    /// </summary>
    public class PaymentReverted
    {
        /// <summary>
        /// Internal Reservation Id
        /// </summary>
        public Guid ReservationId { get; set; }

        /// <summary>
        /// Notes on payment revertion process
        /// </summary>
        public string Notes { get; set; }
    }
}
