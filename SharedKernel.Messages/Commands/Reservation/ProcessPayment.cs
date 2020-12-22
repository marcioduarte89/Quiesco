namespace SharedKernel.Messages.Commands.Reservation
{
    using SharedKernel.Messages.Common;
    using System;

    /// <summary>
    /// Command which starts the payment process
    /// </summary>
    public class ProcessPayment
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
        /// Reservation details for the user
        /// </summary>
        public User User { get; set; }
    }
}
