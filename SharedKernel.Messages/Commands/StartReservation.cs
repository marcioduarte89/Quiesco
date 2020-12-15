﻿namespace SharedKernel.Messages.Commands
{
    using SharedKernel.Messages.Common;
    using System;

    /// <summary>
    /// Saga which starts reservation
    /// </summary>
    public class StartReservation
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
        /// Reservation Check-in
        /// </summary>
        public DateTime CheckIn { get; set; }

        /// <summary>
        /// Reservation Check-out
        /// </summary>
        public DateTime CheckOut { get; set; }

        /// <summary>
        /// Number of occupants
        /// </summary>
        public int NumberOfOccupants { get; set; }

        /// <summary>
        /// Reservation details for the user
        /// </summary>
        public User User { get; set; }
    }
}
