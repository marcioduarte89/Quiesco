namespace Availability.API.Features.UpdatePrices {
    using AutoMapper;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using Output = Availability.API.Models.Output;

    /// <summary>
    /// Update handler
    /// </summary>
    public class UpdatePricesCommandHandler : IRequestHandler<UpdatePricesCommand, Output.Room> {
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper">Mapper instance</param>
        public UpdatePricesCommandHandler(IMapper mapper) {
            _mapper = mapper;
        }

        /// <summary>
        /// Handles commands of type UpdateCommand
        /// </summary>
        /// <param name="request">Update Command</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of type <see cref="Output.Room"/></returns>
        public async Task<Output.Room> Handle(UpdatePricesCommand request, CancellationToken cancellationToken) {
            return _mapper.Map<Output.Room>(null);
        }
    }
}
