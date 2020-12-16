namespace Reservations.API.Features.CreateReservation
{
    using NServiceBus;
    using Reservations.API.Models;
    using SharedKernel.Messages.Commands;
    using SharedKernel.Messages.Events;
    using System.Threading.Tasks;

    /// <summary>
    /// Reservation Saga
    /// </summary>
    public class ReservationSaga : Saga<ReservationSagaData>, 
        IAmStartedByMessages<StartReservation>,
        IHandleMessages<AvailabilityVerified>
    {
        /// <summary>
        /// Defines how the reservation saga is configured
        /// </summary>
        /// <param name="mapper"></param>
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ReservationSagaData> mapper)
        {
            mapper.ConfigureMapping<StartReservation>(message => message.ReservationId)
               .ToSaga(sagaData => sagaData.ReservationId);

            mapper.ConfigureMapping<AvailabilityVerified>(message => message.ReservationId)
            .ToSaga(sagaData => sagaData.ReservationId);
        }

        /// <summary>
        /// Handle the Start reservation step of the Saga
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(StartReservation message, IMessageHandlerContext context)
        {
            await context.Send(new VerifyAvailability()
            {
                ReservationId = message.ReservationId,
                PropertyId = message.PropertyId,
                RoomId = message.RoomId,
                CheckIn = message.CheckIn,
                CheckOut = message.CheckOut
            });
        }

        /// <summary>
        /// Handle the end of the verification of availability
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Handle(AvailabilityVerified message, IMessageHandlerContext context)
        {
            MarkAsComplete();
            return Task.CompletedTask;
        }
    }
}
