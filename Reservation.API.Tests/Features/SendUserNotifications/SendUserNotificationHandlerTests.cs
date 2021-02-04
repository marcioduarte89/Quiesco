namespace Reservations.API.Tests.Features.SendUserNotifications
{
    using Autofac.Extras.Moq;
    using NServiceBus.Testing;
    using NUnit.Framework;
    using Reservation.API.Features.SendUserNotifications;
    using SharedKernel.Messages.Events.Reservation;
    using System;
    using System.Threading.Tasks;

    public class SendUserNotificationHandlerTests
    {
        private AutoMock _mock;
        private TestableMessageHandlerContext _messageHandlerContext;
        private SendUserNotificationHandler _sut;

        [SetUp]
        public void Setup()
        {
            _mock = AutoMock.GetLoose();
            _messageHandlerContext = new TestableMessageHandlerContext();
            _sut = _mock.Create<SendUserNotificationHandler>();
        }

        [Test]
        public async Task SendingUserNotification_PublishesNotificationSent()
        {
            await _sut.Handle(new SendNotification()
            {
                ReservationId = Guid.NewGuid()
            },
            _messageHandlerContext);

            Assert.AreEqual(1, _messageHandlerContext.PublishedMessages.Length);
            Assert.IsInstanceOf<NotificationSent>(_messageHandlerContext.PublishedMessages[0].Message);
        }

        [Test]
        public async Task SendingUserNotification_PublishesNotificationSentForCancellation()
        {
            await _sut.Handle(new NotifyReservationCancellation()
            {
                ReservationId = Guid.NewGuid()
            },
            _messageHandlerContext);

            Assert.AreEqual(1, _messageHandlerContext.PublishedMessages.Length);
            Assert.IsInstanceOf<NotificationSentForCancellation>(_messageHandlerContext.PublishedMessages[0].Message);
        }
    }
}
