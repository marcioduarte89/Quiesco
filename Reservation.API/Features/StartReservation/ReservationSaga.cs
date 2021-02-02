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
        IHandleMessages<AvailabilityVerified>,
        IHandleMessages<PropertyExistanceVerified>,
        IHandleMessages<ReservationCancelled>,
        IHandleMessages<ReservationCreated>,
        IHandleMessages<PaymentProcessed>,
        IHandleMessages<PaymentProcessedFailed>,
        IHandleMessages<ReservationCompleted>,
        IHandleMessages<ReservationCompletionFailed>,
        IHandleMessages<PaymentReverted>,
        IHandleMessages<BookingAdded>,
        IHandleMessages<NotificationSent>,
        IHandleMessages<NotificationSentForCancellation>
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

            mapper.ConfigureMapping<PropertyExistanceVerified>(message => message.ReservationId)
            .ToSaga(sagaData => sagaData.ReservationId);

            mapper.ConfigureMapping<ReservationCancelled>(message => message.ReservationId)
            .ToSaga(sagaData => sagaData.ReservationId);

            mapper.ConfigureMapping<ReservationCreated>(message => message.ReservationId)
            .ToSaga(sagaData => sagaData.ReservationId);

            mapper.ConfigureMapping<PaymentProcessed>(message => message.ReservationId)
            .ToSaga(sagaData => sagaData.ReservationId);

            mapper.ConfigureMapping<PaymentProcessedFailed>(message => message.ReservationId)
            .ToSaga(sagaData => sagaData.ReservationId);

            mapper.ConfigureMapping<ReservationCompleted>(message => message.ReservationId)
            .ToSaga(sagaData => sagaData.ReservationId);

            mapper.ConfigureMapping<ReservationCompletionFailed>(message => message.ReservationId)
            .ToSaga(sagaData => sagaData.ReservationId);

            mapper.ConfigureMapping<PaymentReverted>(message => message.ReservationId)
            .ToSaga(sagaData => sagaData.ReservationId);

            mapper.ConfigureMapping<BookingAdded>(message => message.ReservationId)
            .ToSaga(sagaData => sagaData.ReservationId);

            mapper.ConfigureMapping<NotificationSent>(message => message.ReservationId)
            .ToSaga(sagaData => sagaData.ReservationId);

            mapper.ConfigureMapping<NotificationSentForCancellation>(message => message.ReservationId)
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

            await context.SendLocal(new VerifyPropertyExists()
            {
                ReservationId = Data.ReservationId,
                PropertyId = Data.PropertyId,
                RoomId = Data.RoomId
            });
        }

        /// <summary>
        /// Handle the validation of whether a property exists or not
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(PropertyExistanceVerified message, IMessageHandlerContext context)
        {
            if (!message.Exists)
            {
                // If property hasn't been verified doesn't even need to do anything, just mark it as complete
                MarkAsComplete();
                return;
            }

            await context.SendLocal(new CreateReservation()
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
            // need to verify availability again, as it might just have been booked..
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
                Data.ReasonForCancellation = "Room not available";
                // Compensation
                // Could have created before checking availability, but that would mean an extra write
                await context.SendLocal(new CancelReservation()
                {
                    ReservationId = Data.ReservationId,
                    Reason = Data.ReasonForCancellation // This should come from message
                });

                return;
            }

            // still need to define how the payment will look like..
            await context.SendLocal(new ProcessPayment()
            {
                ReservationId = Data.ReservationId,
                PropertyId = Data.PropertyId,
                RoomId = Data.RoomId
            });
        }

        /// <summary>
        /// Handles payment process
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(PaymentProcessed message, IMessageHandlerContext context)
        {
            await context.Send(new AddBooking()
            {
                ReservationId = Data.ReservationId
            });
        }

        /// <summary>
        /// Handles booking being added
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(BookingAdded message, IMessageHandlerContext context)
        {
            await context.SendLocal(new CompleteReservation()
            {
                ReservationId = Data.ReservationId
            });
        }

        /// <summary>
        /// Handles booking failed to be added
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(AddBookingFailed message, IMessageHandlerContext context)
        {
            await context.SendLocal(new RevertPaymentProcess()
            {
                ReservationId = Data.ReservationId
            });
        }

        /// <summary>
        /// Handles the event when payment failed to be processed
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(PaymentProcessedFailed message, IMessageHandlerContext context)
        {
            await context.SendLocal(new CancelReservation()
            {
                ReservationId = Data.ReservationId,
                Reason = message.Error
            });
        }

        /// <summary>
        /// Handles event when reservation was completed
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(ReservationCompleted message, IMessageHandlerContext context)
        {
            await context.Publish(new SendNotification()
            {
                ReservationId = Data.ReservationId,
                Notes = "Reservation complete successfully", //add more details here later
                User = Data.User
            });
        }

        /// <summary>
        /// Handles event when the reservation failed to complete
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(ReservationCompletionFailed message, IMessageHandlerContext context)
        {
            await context.SendLocal(new RevertPaymentProcess()
            {
                ReservationId = Data.ReservationId,
                PropertyId = Data.PropertyId,
                RoomId = Data.RoomId,
                Notes = message.Reason
            });
        }

        /// <summary>
        /// Handles compensation step to revert payment process
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(PaymentReverted message, IMessageHandlerContext context)
        {
            await context.SendLocal(new CancelReservation()
            {
                ReservationId = Data.ReservationId,
                Reason = message.Notes
            });
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
                User = Data.User,
                CancellationReason = Data.ReasonForCancellation
            });
        }

        /// <summary>
        /// Handles cases of notification have been sent
        /// Only completes the saga when both the property and user has received notification
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Handle(NotificationSent message, IMessageHandlerContext context)
        {
            if (message.PropertyNotificationSent && !Data.PropertyNotificationSent)
            {
                Data.PropertyNotificationSent = message.PropertyNotificationSent;
            }

            if(message.UserNotificationSent && !Data.UserNotificationSent)
            {
                Data.UserNotificationSent = message.UserNotificationSent;
            }

            // Scather-gather on notifications
            if(Data.PropertyNotificationSent && Data.UserNotificationSent)
            {
                MarkAsComplete();
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Handles event when notification of cancellation has been sent to the user
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Handle(NotificationSentForCancellation message, IMessageHandlerContext context)
        {
            if (message.UserNotificationSent)
            {
                MarkAsComplete();
            }

            return Task.CompletedTask;
        }
    }
}
