namespace Availability.API.Features.Get {
    using AutoMapper;
    using MediatR;
    using Availability.API.Models.Output;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Update command handler
    /// </summary>
    public class GetRequestHandler : IRequestHandler<GetRequest, Room> {
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Products Context</param>
        /// <param name="mapper">Mapper instance</param>
        public GetRequestHandler(IMapper mapper) {
            _mapper = mapper;
        }

        /// <summary>
        /// Handles commands of type GetCommand
        /// </summary>
        /// <param name="request">GetCommand</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of type <see cref="Room"/></returns>
        public async Task<Room> Handle(GetRequest request, CancellationToken cancellationToken) {
            return _mapper.Map<Room>(null);
        }
    }
}
