namespace SharedKernel.Messages.Events.Reservation
{
    using System;

    /// <summary>
    /// Payment processed event
    /// </summary>
    public class PaymentProcessed
    {
        /// <summary>
        /// Property Id
        /// </summary>
        public Guid ReservationId { get; set; }
    }
}
