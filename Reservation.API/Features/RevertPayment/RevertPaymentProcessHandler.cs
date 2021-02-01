namespace Reservation.API.Features.RevertPayment
{
    using NServiceBus;
    using SharedKernel.Messages.Commands.Reservation;
    using SharedKernel.Messages.Events.Reservation;
    using System.Threading.Tasks;

    /// <summary>
    /// Reverts payment process
    /// </summary>
    public class RevertPaymentProcessHandler : IHandleMessages<RevertPaymentProcess>
    {
        /// <summary>
        /// Handles the process of payment
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(RevertPaymentProcess message, IMessageHandlerContext context)
        {
            await context.Publish(new PaymentReverted()
            {
                ReservationId = message.ReservationId,
                Notes = "Payment has been reverted"
            });
        }
    }
}
