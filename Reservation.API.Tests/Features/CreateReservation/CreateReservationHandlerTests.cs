namespace Reservations.API.Tests.Features.CreateReservation
{
    using Autofac.Extras.Moq;
    using Moq;
    using NServiceBus.Testing;
    using NUnit.Framework;
    using Reservation.API.Features.CreateReservation;
    using Reservations.Core.Models;
    using Reservations.Infrastructure.Data.Repositories;
    using SharedKernel.Messages.Commands.Reservation;
    using SharedKernel.Messages.Events.Reservation;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateReservationHandlerTests
    {
        private AutoMock _mock;
        private TestableMessageHandlerContext _messageHandlerContext;
        private CreateReservationHandler _sut;
        private Mock<IReadReservationRepository> _readRepository;

        [SetUp]
        public void Setup()
        {
            _mock = AutoMock.GetLoose();
            _messageHandlerContext = new TestableMessageHandlerContext();
            _sut = _mock.Create<CreateReservationHandler>();
            _readRepository = _mock.Mock<IReadReservationRepository>();
        }

        [Test]
        public async Task CreateReservation_ValidReservationData_PublishesReservationCreated()
        {
            _readRepository
                .Setup(x => x.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Reservation());

            await _sut.Handle(new CreateReservation()
            {
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2),
                NumberOfOccupants = 1,
                PropertyId = 1,
                RoomId = 2,
                User = new SharedKernel.Messages.Common.User()
                {
                    Name = "Name",
                    LastName = "Last Name",
                    UserEmail = "name@lastName.gmail",
                    PhoneNumber = "Name",
                },
                ReservationId = Guid.NewGuid()
            },
            _messageHandlerContext);

            Assert.AreEqual(1, _messageHandlerContext.PublishedMessages.Length);
            Assert.IsInstanceOf<ReservationCreated>(_messageHandlerContext.PublishedMessages[0].Message);
        }
    }
}
