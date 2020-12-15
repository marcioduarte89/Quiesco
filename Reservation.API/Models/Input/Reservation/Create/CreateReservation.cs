namespace Reservation.API.Models.Input.Reservation.Create
{
    using Reservations.API.Models.Input.Common;
    using System;

    /// <summary>
    /// Creates a new reservation
    /// </summary>
    public class CreateReservation
    {
        /// <summary>
        /// Property Id
        /// </summary>
        public int PropertyId { get; set; }

        /// <summary>
        /// Room Id
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// User details
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Checkin date
        /// </summary>
        public DateTime CheckIn { get; set; }

        /// <summary>
        /// Checkout date
        /// </summary>
        public DateTime CheckOut { get; set; }

        /// <summary>
        /// Number of occupants for the reservation
        /// </summary>
        public int NumberOfOccupants { get; set; }
    }
}
