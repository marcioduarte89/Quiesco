namespace Reservations.API.Tests.Features.SendPropertyNotifications
{
    using Autofac.Extras.Moq;
    using NServiceBus.Testing;
    using NUnit.Framework;
    using Reservation.API.Features.SendPropertyNotifications;
    using SharedKernel.Messages.Events.Reservation;
    using System;
    using System.Threading.Tasks;

    public class SendUserNotificationHandlerTests
    {
        private AutoMock _mock;
        private TestableMessageHandlerContext _messageHandlerContext;
        private SendPropertyNotificationHandler _sut;

        [SetUp]
        public void Setup()
        {
            _mock = AutoMock.GetLoose();
            _messageHandlerContext = new TestableMessageHandlerContext();
            _sut = _mock.Create<SendPropertyNotificationHandler>();
        }

        [Test]
        public async Task SendingPropertyNotification_PublishesNotificationSent()
        {
            await _sut.Handle(new SendNotification()
            {
                ReservationId = Guid.NewGuid()
            },
            _messageHandlerContext);

            Assert.AreEqual(1, _messageHandlerContext.PublishedMessages.Length);
            Assert.IsInstanceOf<NotificationSent>(_messageHandlerContext.PublishedMessages[0].Message);
        }
    }
}
