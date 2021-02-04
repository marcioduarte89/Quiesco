namespace SharedKernel.Messages.Events.Reservation
{
    using System;

    /// <summary>
    /// Payment processed failed event
    /// </summary>
    public class PaymentProcessedFailed
    {
        /// <summary>
        /// Property Id
        /// </summary>
        public Guid ReservationId { get; set; }

        /// <summary>
        /// Error description if payment was not processed
        /// </summary>
        public string Error { get; set; }
    }
}
