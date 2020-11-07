namespace Reservation.Common.Exceptions {
    using System;

    /// <summary>
    /// Product Exception for All product domain entities
    /// </summary>
    public class ReservationException : Exception {
        /// <summary>
        /// Code for when dealing invalid Data
        /// </summary>
        public const int INVALID_DATA = 1;

        /// <summary>
        /// Code for when dealing with not found items
        /// </summary>
        public const int NOT_FOUND = 2;

        /// <summary>
        /// Code for when dealing with generic errors
        /// </summary>
        public const int SERVER_ERROR = 3;

        /// <summary>
        /// Exception detail Code
        /// </summary>
        public int Detail { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="detail">Exception detail code</param>
        /// <param name="message">Exception Message</param>
        public ReservationException(int detail, string message) : base(message) {
            Detail = detail;
        }
    }
}
