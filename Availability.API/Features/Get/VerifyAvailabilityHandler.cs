namespace Availability.API.Features.Get
{
    using NServiceBus;
    using SharedKernel.Messages.Commands;
    using SharedKernel.Messages.Events;
    using System.Threading.Tasks;

    /// <summary>
    /// Verifies Availability
    /// </summary>
    public class VerifyAvailabilityHandler : IHandleMessages<VerifyAvailability>
    {
        /// <summary>
        /// Handles requests of type <see cref="VerifyAvailability"/>
        /// </summary>
        /// <param name="message">Message type</param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(VerifyAvailability message, IMessageHandlerContext context)
        {
            await context.Publish(new AvailabilityVerified()
            {
                HasAvailability = true,
                ReservationId = message.ReservationId
            });
        }
    }
}
