namespace Availability.API.Tests.Features.AddBooking
{
    using Autofac.Extras.Moq;
    using Availability.API.Features.CheckAvailability;
    using Availability.Infrastructure.Data.Repositories;
    using Moq;
    using NServiceBus.Testing;
    using NUnit.Framework;
    using SharedKernel.Messages.Commands.Reservation;
    using SharedKernel.Messages.Events.Reservation;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class VerifyAvailabilityHandlerTests
    {
        private AutoMock _mock;
        private TestableMessageHandlerContext _messageHandlerContext;
        private VerifyAvailabilityHandler _sut;
        private Mock<IRoomRepository> _repository;
        private Mock<IGlobalReadRepository> _globalRepository;

        [SetUp]
        public void Setup()
        {
            _mock = AutoMock.GetLoose();
            _messageHandlerContext = new TestableMessageHandlerContext();
            _sut = _mock.Create<VerifyAvailabilityHandler>();
            _repository = _mock.Mock<IRoomRepository>();
            _globalRepository = _mock.Mock<IGlobalReadRepository>();
        }

        [Test]
        public async Task VerifyAvailability_HasNoAvailability_PublishesAvailabilityVerifiedWithNoAvailability()
        {
            _globalRepository.Setup(x => x.HasRoom(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);

            await _sut.Handle(new VerifyAvailability()
            {
                PropertyId = 1,
                RoomId = 1,
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2),
                ReservationId = Guid.NewGuid()
            }, _messageHandlerContext);

            Assert.AreEqual(1, _messageHandlerContext.PublishedMessages.Length);
            Assert.IsInstanceOf<AvailabilityVerified>(_messageHandlerContext.PublishedMessages[0].Message);
            var message = _messageHandlerContext.PublishedMessages[0].Message as AvailabilityVerified;
            Assert.IsFalse(message.HasAvailability);
        }

        [Test]
        public async Task VerifyAvailability_HasAvailability_PublishesAvailabilityVerifiedAsAvailability()
        {
            _globalRepository.Setup(x => x.HasRoom(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
            _repository.Setup(x => x.CheckAvailability(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            await _sut.Handle(new VerifyAvailability()
            {
                PropertyId = 1,
                RoomId = 1,
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2),
                ReservationId = Guid.NewGuid()
            }, _messageHandlerContext);

            Assert.AreEqual(1, _messageHandlerContext.PublishedMessages.Length);
            Assert.IsInstanceOf<AvailabilityVerified>(_messageHandlerContext.PublishedMessages[0].Message);
            var message = _messageHandlerContext.PublishedMessages[0].Message as AvailabilityVerified;
            Assert.IsTrue(message.HasAvailability);
        }
    }
}
