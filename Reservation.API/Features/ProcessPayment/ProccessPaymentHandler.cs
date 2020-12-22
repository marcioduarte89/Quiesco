namespace Reservation.API.Features.ProcessPayment
{
    using NServiceBus;
    using SharedKernel.Messages.Commands.Reservation;
    using SharedKernel.Messages.Events.Reservation;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Processes payment
    /// </summary>
    public class ProccessPaymentHandler : IHandleMessages<ProcessPayment>
    {
        /// <summary>
        /// Handles the process of payment
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(ProcessPayment message, IMessageHandlerContext context)
        {
            var randomVal = new Random().Next(1000);

            if((double)(randomVal % 2) == 0)
            {
                await context.Publish(new PaymentProcessed()
                {
                    ReservationId = message.ReservationId
                });
            }
            else
            {
                await context.Publish(new PaymentProcessedFailed()
                {
                    ReservationId = message.ReservationId,
                    Error = "Not enough funds"
                });
            }

            throw new System.NotImplementedException();
        }
    }
}
