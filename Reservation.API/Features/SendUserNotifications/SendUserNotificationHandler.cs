namespace Reservation.API.Features.SendUserNotifications
{
    using NServiceBus;
    using SharedKernel.Messages.Events.Reservation;
    using System.Threading.Tasks;

    /// <summary>
    /// Handles notifications to be sent to users
    /// </summary>
    public class SendUserNotificationHandler : IHandleMessages<SendNotification>, IHandleMessages<NotifyReservationCancellation>
    {
        /// <summary>
        /// Handles the notifications to be sent to users
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(SendNotification message, IMessageHandlerContext context)
        {
            await context.Publish(new NotificationSent()
            {
                UserNotificationSent = true,
                ReservationId = message.ReservationId
            });
        }

        public async Task Handle(NotifyReservationCancellation message, IMessageHandlerContext context)
        {
            await context.Publish(new NotificationSentForCancellation()
            {
                UserNotificationSent = true,
                ReservationId = message.ReservationId
            });
        }
    }
}
