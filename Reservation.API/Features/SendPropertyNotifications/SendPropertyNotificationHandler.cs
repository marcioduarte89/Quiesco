namespace Reservation.API.Features.SendPropertyNotifications
{
    using NServiceBus;
    using SharedKernel.Messages.Events.Reservation;
    using System.Threading.Tasks;

    /// <summary>
    /// Handles notifications to be sent to the property
    /// </summary>
    public class SendPropertyNotificationHandler : IHandleMessages<SendNotification>
    {
        /// <summary>
        /// Handles the notifications to be sent to property
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(SendNotification message, IMessageHandlerContext context)
        {
            await context.Publish(new NotificationSent()
            {
                PropertyNotificationSent = true,
                ReservationId = message.ReservationId
            });
        }
    }
}
