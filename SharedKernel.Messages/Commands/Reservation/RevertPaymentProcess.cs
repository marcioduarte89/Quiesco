namespace SharedKernel.Messages.Commands.Reservation
{
    using System;

    /// <summary>
    /// Command which reverts a payment which has been processed
    /// </summary>
    public class RevertPaymentProcess
    {
        /// <summary>
        /// Internal Reservation Id
        /// </summary>
        public Guid ReservationId { get; set; }

        /// <summary>
        /// Property Id
        /// </summary>
        public int PropertyId { get; set; }

        /// <summary>
        /// Room Id
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        public string Notes { get; set; }
    }
}
