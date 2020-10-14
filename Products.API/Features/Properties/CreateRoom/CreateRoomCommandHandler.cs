namespace Products.API.Features.Properties.CreateRoom
{
    using AutoMapper;
    using Common.Exceptions;
    using Core.Models;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Products.Infrastructure.Data;
    using System.Threading;
    using System.Threading.Tasks;
    using Output = Products.API.Models.Output;

    /// <summary>
    /// Create room command handler
    /// Handles the creation of rooms within a property for Products API
    /// </summary>
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Output.Room>
    {
        private readonly ProductsContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Products Context</param>
        /// <param name="mapper">Mapper instance</param>
        public CreateRoomCommandHandler(ProductsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles commands of type CreateRoomCommand and creates Rooms
        /// </summary>
        /// <param name="request">Create room command</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of type <see cref="Output.Room"/></returns>
        public async Task<Output.Room> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var property = await _context.Properties.Include(x => x.Rooms).FirstOrDefaultAsync(x => x.Id == request.PropertyId, cancellationToken);

            if (property == null)
            {
                throw new ProductException(ProductException.NOT_FOUND, $"property with Id {request.PropertyId} not found");
            }

            var room = new Room(request.AccommodationType, request.NrOfOccupants);

            property.AddRoom(room);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<Output.Room>(room);
        }
    }
}
