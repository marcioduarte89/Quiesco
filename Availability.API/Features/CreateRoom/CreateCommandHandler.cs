namespace Availability.API.Features.CreateRoom
{
    using System.Linq;
    using AutoMapper;
    using Availability.Infrastructure.Data.Repositories;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Exceptions;
    using Core.Models;
    using Output = Models.Output;

    /// <summary>
    /// Create command handler
    /// </summary>
    public class CreateCommandHandler : IRequestHandler<CreateCommand, Output.Room> {
        private readonly IMapper _mapper;
        private readonly IRoomsRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="repository">rooms repository</param>
        public CreateCommandHandler(IMapper mapper, IRoomsRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Handles commands of type CreateCommand
        /// </summary>
        /// <param name="request">Create property command</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of type <see cref="Output.Room"/></returns>
        public async Task<Output.Room> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var existingRoom = await _repository.Get(request.PropertyId, request.RoomId, cancellationToken);

            if(existingRoom != null)
            {
                throw new AvailabilityException(AvailabilityException.INVALID_DATA, $"The room with id {request.RoomId} in property id {request.PropertyId} already exists");
            }

            var room = Room.Create(request.PropertyId, request.RoomId, request.DefaultPrice);

            // In case there were pre-bookings before the launch of the venue
            room.SetDatePrices(request.Prices?.Select(x => Price.Create(x.Date, x.Value)).ToList());
            room.AddBookings(request.BookedSlots);

            await _repository.Save(room, cancellationToken);

            return _mapper.Map<Output.Room>(room);
        }
    }
}
