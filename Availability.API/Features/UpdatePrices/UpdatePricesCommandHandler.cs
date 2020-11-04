namespace Availability.API.Features.UpdatePrices {
    using System.Linq;
    using AutoMapper;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using Availability.Infrastructure.Data.Repositories;
    using Common.Exceptions;
    using Core.Models;
    using Output = Availability.API.Models.Output;

    /// <summary>
    /// Update handler
    /// </summary>
    public class UpdatePricesCommandHandler : IRequestHandler<UpdatePricesCommand, Output.Room> {
        private readonly IMapper _mapper;
        private readonly IRoomsRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="repository">Rooms repository</param>
        public UpdatePricesCommandHandler(IMapper mapper, IRoomsRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Handles commands of type UpdateCommand
        /// </summary>
        /// <param name="request">Update Command</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of type <see cref="Output.Room"/></returns>
        public async Task<Output.Room> Handle(UpdatePricesCommand request, CancellationToken cancellationToken) {

            var existingRoom = await _repository.Get(request.PropertyId, request.RoomId, cancellationToken);

            if (existingRoom == null)
            {
                throw new AvailabilityException(AvailabilityException.NOT_FOUND, $"Could not find room with id {request.RoomId} in property id {request.PropertyId}");
            }

            existingRoom.SetDatePrices(request.Prices?.Select(x => Price.Create(x.Date, x.Value)).ToList());

            await _repository.Save(existingRoom, cancellationToken);

            return _mapper.Map<Output.Room>(existingRoom);
        }
    }
}
