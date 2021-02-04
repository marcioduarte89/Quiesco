namespace SharedKernel.Messages.Events.Reservation
{
    using SharedKernel.Messages.Common;
    using System;

    /// <summary>
    /// Event to start notification
    /// </summary>
    public class SendNotification
    {
        /// <summary>
        /// Internal Reservation Id
        /// </summary>
        public Guid ReservationId { get; set; }

        /// <summary>
        /// Notification notes
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// User details
        /// </summary>
        public User User { get; set; }
    }
}
