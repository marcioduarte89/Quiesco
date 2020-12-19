namespace Reservation.API.Features.CreateReservation
{
    using NServiceBus;
    using SharedKernel.Messages.Commands.Reservation;
    using System.Threading.Tasks;

    /// <summary>
    /// Verifies Availability
    /// </summary>
    public class CreateReservationHandler : IHandleMessages<CreateReservation>
    {
        ///// <summary>
        ///// Constructor
        ///// </summary>
        ///// <param name="repository">Room repository</param>
        ///// <param name="globalReadRepository">Global read repository</param>
        //public CreateReservationHandler(IRoomRepository repository, IGlobalReadRepository globalReadRepository)
        //{
        //    _repository = repository;
        //    _globalReadRepository = globalReadRepository;
        //}

        public Task Handle(CreateReservation message, IMessageHandlerContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
