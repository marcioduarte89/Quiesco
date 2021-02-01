namespace SharedKernel.Messages.Commands.Reservation
{
    using System;

    public class AddBooking
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
        /// Checkin date
        /// </summary>
        public DateTime CheckIn { get; set; }

        /// <summary>
        /// Checkout date
        /// </summary>
        public DateTime CheckOut { get; set; }
    }
}
