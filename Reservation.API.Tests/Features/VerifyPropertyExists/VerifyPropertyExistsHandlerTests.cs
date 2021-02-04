namespace Reservations.API.Tests.Features.VerifyPropertyExists
{
    using Autofac.Extras.Moq;
    using Moq;
    using Nest;
    using NServiceBus.Testing;
    using NUnit.Framework;
    using Reservation.API.Features.CreateReservation;
    using SharedKernel.Messages.Commands.Reservation;
    using SharedKernel.Messages.Events.Reservation;
    using System;
    using System.Threading.Tasks;

    public class VerifyPropertyExistsHandlerTests
    {
        private AutoMock _mock;
        private TestableMessageHandlerContext _messageHandlerContext;
        private VerifyPropertyExistsHandler _sut;
        private Mock<IElasticClient> _elasticClient;

        [SetUp]
        public void Setup()
        {
            _mock = AutoMock.GetLoose();
            _messageHandlerContext = new TestableMessageHandlerContext();
            _sut = _mock.Create<VerifyPropertyExistsHandler>();
            _elasticClient = _mock.Mock<IElasticClient>();
    }

        [Test]
        public async Task VerifyPropertyExists_PropertyDoesNotExist_PublishesPropertyExistanceVerified()
        {
            await _sut.Handle(new VerifyPropertyExists()
            {
                ReservationId = Guid.NewGuid()
            },
            _messageHandlerContext);

            Assert.AreEqual(1, _messageHandlerContext.PublishedMessages.Length);
            Assert.IsInstanceOf<PropertyExistanceVerified>(_messageHandlerContext.PublishedMessages[0].Message);
        }
    }
}
