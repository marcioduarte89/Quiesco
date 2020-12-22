namespace SharedKernel.Messages.Events.Reservation
{
    using System;

    /// <summary>
    /// Notification event for when cancellation has been sent
    /// </summary>
    public class NotificationSentForCancellation
    {
        /// <summary>
        /// Internal Reservation Id
        /// </summary>
        public Guid ReservationId { get; set; }

        /// <summary>
        /// Wheter the notification has been sent to the user
        /// </summary>
        public bool UserNotificationSent { get; set; }
    }
}
