namespace Reservations.API.Tests.Features.CancelReservation
{
    using Autofac.Extras.Moq;
    using Moq;
    using NServiceBus.Testing;
    using NUnit.Framework;
    using Reservation.API.Features.CancelReservation;
    using Reservation.Common.Exceptions;
    using Reservations.Core.Models;
    using Reservations.Infrastructure.Data.Repositories;
    using SharedKernel.Messages.Commands.Reservation;
    using SharedKernel.Messages.Events.Reservation;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class CancelReservationHandlerTests
    {
        private AutoMock _mock;
        private TestableMessageHandlerContext _messageHandlerContext;
        private CancelReservationHandler _sut;
        private Mock<IReadReservationRepository> _readRepository;

        [SetUp]
        public void Setup()
        {
            _mock = AutoMock.GetLoose();
            _messageHandlerContext = new TestableMessageHandlerContext();
            _sut = _mock.Create<CancelReservationHandler>();
            _readRepository = _mock.Mock<IReadReservationRepository>();
        }

        [Test]
        public void CancelReservation_NoExistingReservation_ThrowsReservationException()
        {
            Assert.ThrowsAsync<ReservationException>(async () =>
            {
                await _sut.Handle(new CancelReservation()
                {
                    ReservationId = Guid.NewGuid()
                },
                _messageHandlerContext);
            });
        }

        [Test]
        public async Task CancelReservation_ExistingReservation_PublishesCancelledReservation()
        {
            _readRepository
                .Setup(x => x.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Reservation());

            await _sut.Handle(new CancelReservation()
            {
                Reason = "Some reason",
                ReservationId = Guid.NewGuid()
            },
            _messageHandlerContext);

            Assert.AreEqual(1, _messageHandlerContext.PublishedMessages.Length);
            Assert.IsInstanceOf<ReservationCancelled>(_messageHandlerContext.PublishedMessages[0].Message);
        }
    }
}
