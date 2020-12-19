namespace Reservations.API.Features.StartReservation
{
    using NServiceBus;
    using Reservations.API.Models;
    using SharedKernel.Messages.Commands.Reservation;
    using SharedKernel.Messages.Events.Reservation;
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
            Data.PropertyId = message.PropertyId;
            Data.RoomId = message.RoomId;
            Data.CheckIn = message.CheckIn;
            Data.CheckOut = message.CheckOut;
            Data.NumberOfOccupants = message.NumberOfOccupants;
            Data.User = message.User;

            await context.Send(new CreateReservation()
            {
                ReservationId = Data.ReservationId,
                PropertyId = Data.PropertyId,
                RoomId = Data.RoomId,
                CheckIn = Data.CheckIn,
                CheckOut = Data.CheckOut,
                NumberOfOccupants = Data.NumberOfOccupants,
                User = Data.User
            });
        }

        /// <summary>
        /// Handle the creation of a reservation
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(ReservationCreated message, IMessageHandlerContext context)
        {
            await context.Send(new VerifyAvailability()
            {
                ReservationId = Data.ReservationId,
                PropertyId = Data.PropertyId,
                RoomId = Data.RoomId,
                CheckIn = Data.CheckIn,
                CheckOut = Data.CheckOut
            });
        }

        /// <summary>
        /// Handle the end of the verification of availability
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(AvailabilityVerified message, IMessageHandlerContext context)
        {
            Data.HasAvailability = message.HasAvailability;

            if (!message.HasAvailability)
            {
                // Could have created before checking availability, but that would mean an extra write
                await context.Send(new CancelReservation()
                {
                    ReservationId = Data.ReservationId
                });

                MarkAsComplete();
                return;
            }
        }

        /// <summary>
        /// Handle the end of the verification of availability
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(ReservationCancelled message, IMessageHandlerContext context)
        {
            await context.Publish(new NotifyReservationCancellation()
            {
                ReservationId = Data.ReservationId,
                PropertyId = Data.PropertyId,
                RoomId = Data.RoomId,
                CheckIn = Data.CheckIn,
                CheckOut = Data.CheckOut,
                User = Data.User
            });

            MarkAsComplete();
        }
    }
}
