namespace SharedKernel.Messages.Events.Reservation
{
    using System;

    /// <summary>
    /// Notification sent event
    /// </summary>
    public class NotificationSent
    {
        /// <summary>
        /// Internal Reservation Id
        /// </summary>
        public Guid ReservationId { get; set; }

        /// <summary>
        /// Whether the notification has been sent to the property
        /// </summary>
        public bool PropertyNotificationSent { get; set; }

        /// <summary>
        /// Wheter the notification has been sent to the user
        /// </summary>
        public bool UserNotificationSent { get; set; }
    }
}
