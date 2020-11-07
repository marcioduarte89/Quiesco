using NServiceBus;
using Reservations.API.Models;
using SharedKernel.Messages.Commands;
using System.Threading.Tasks;

namespace Reservations.API.Features.CreateReservation
{
    /// <summary>
    /// Reservation Saga
    /// </summary>
    public class ReservationSaga : Saga<ReservationSagaData>, IAmStartedByMessages<StartReservation>
    {
        /// <summary>
        /// Defines how the reservation saga is configured
        /// </summary>
        /// <param name="mapper"></param>
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ReservationSagaData> mapper)
        {
            mapper.ConfigureMapping<StartReservation>(message => message.ReservationId)
               .ToSaga(sagaData => sagaData.ReservationId);
        }

        /// <summary>
        /// Handle the Start reservation step of the Saga
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Handle(StartReservation message, IMessageHandlerContext context)
        {
            MarkAsComplete();
            return Task.CompletedTask;
        }
    }
}
