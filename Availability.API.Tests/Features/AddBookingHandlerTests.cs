using Autofac;
using Autofac.Extras.Moq;
using Availability.API.Features.CheckAvailability;
using Availability.Common.Exceptions;
using Availability.Core.Models;
using Availability.Infrastructure.Data.Repositories;
using Moq;
using NServiceBus.Testing;
using NUnit.Framework;
using SharedKernel.Messages.Commands.Reservation;
using SharedKernel.Messages.Events.Reservation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Availability.API.Tests.Features
{
    public class AddBookingHandlerTests
    {
        private AutoMock _mock;
        private TestableMessageHandlerContext _messageHandlerContext;
        private AddBookingHandler _sut;
        private Mock<IRoomRepository> _repository;

        [SetUp]
        public void Setup()
        {
            _mock = AutoMock.GetLoose();
            _messageHandlerContext = new TestableMessageHandlerContext();
            _sut = _mock.Create<AddBookingHandler>();
            _repository = _mock.Mock<IRoomRepository>();
        }

        [Test]
        public void AddBooking_RoomDoesNotExist_ThrowAvailabilityException()
        {
            Assert.ThrowsAsync<AvailabilityException>(async () =>
            {
                await _sut.Handle(new AddBooking()
                {
                    PropertyId = 1,
                    RoomId = 1,
                    CheckIn = DateTime.Now.AddDays(1),
                    CheckOut = DateTime.Now.AddDays(2),
                    ReservationId = Guid.NewGuid()
                },
                _messageHandlerContext
                );
            });
        }

        [Test]
        public async Task AddBooking_NoAvailability_PublishesAddBookingFailed()
        {
            var room = Room.Create(1, 1, 10);
            room.AddBookings(new DateTime[] { DateTime.Now.AddDays(1), DateTime.Now.AddDays(2) });

            var repository = _mock.Mock<IRoomRepository>();
            repository
                .Setup(x => x.Get(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(room);
            _sut = _mock.Create<AddBookingHandler>(new TypedParameter(typeof(IRoomRepository), repository.Object));
            
            await _sut.Handle(new AddBooking()
            {
                PropertyId = 1,
                RoomId = 1,
                CheckIn = DateTime.Now.AddDays(1),
                CheckOut = DateTime.Now.AddDays(2),
                ReservationId = Guid.NewGuid()
            },_messageHandlerContext);

            Assert.AreEqual(1, _messageHandlerContext.PublishedMessages.Length);
            Assert.IsInstanceOf<AddBookingFailed>(_messageHandlerContext.PublishedMessages[0].Message);
        }
    }
}
