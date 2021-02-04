namespace Reservations.API.Tests.Features.RevertPayment
{
    using Autofac.Extras.Moq;
    using NServiceBus.Testing;
    using NUnit.Framework;
    using Reservation.API.Features.RevertPayment;
    using SharedKernel.Messages.Commands.Reservation;
    using SharedKernel.Messages.Events.Reservation;
    using System;
    using System.Threading.Tasks;

    public class RevertPaymentProcessHandlerTests
    {
        private AutoMock _mock;
        private TestableMessageHandlerContext _messageHandlerContext;
        private RevertPaymentProcessHandler _sut;

        [SetUp]
        public void Setup()
        {
            _mock = AutoMock.GetLoose();
            _messageHandlerContext = new TestableMessageHandlerContext();
            _sut = _mock.Create<RevertPaymentProcessHandler>();
        }

        [Test]
        public async Task RevertingPayment_PublishesRevertPaymentProcess()
        {
            await _sut.Handle(new RevertPaymentProcess()
            {
                ReservationId = Guid.NewGuid()
            },
            _messageHandlerContext);

            Assert.AreEqual(1, _messageHandlerContext.PublishedMessages.Length);
            Assert.IsInstanceOf<PaymentReverted>(_messageHandlerContext.PublishedMessages[0].Message);
        }
    }
}
