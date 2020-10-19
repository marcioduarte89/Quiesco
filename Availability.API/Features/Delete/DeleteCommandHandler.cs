namespace Availability.API.Features.Delete {
    using AutoMapper;
    using MediatR;
    using Availability.API.Models.Output;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Delete handler
    /// </summary>
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, Room> {
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper">Mapper instance</param>
        public DeleteCommandHandler(IMapper mapper) {
            _mapper = mapper;
        }

        /// <summary>
        /// Handles commands of type DeleteCommand 
        /// </summary>
        /// <param name="request">delete command</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of type <see cref="Room"/></returns>
        public async Task<Room> Handle(DeleteCommand request, CancellationToken cancellationToken) {
            return _mapper.Map<Room>(null);
        }
    }
}
