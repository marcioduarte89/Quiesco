namespace Products.API.Features.Properties.UpdateRoom
{
    using AutoMapper;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Products.API.Models.Output;
    using Products.Common.Exceptions;
    using Products.Infrastructure.Data;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Update room command handler
    /// Handles the update of rooms for a property within the Products API
    /// </summary>
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Room>
    {
        private readonly ProductsContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Products Context</param>
        /// <param name="mapper">Mapper instance</param>
        public UpdateRoomCommandHandler(ProductsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles commands of type UpdateRoomCommand and updates rooms
        /// </summary>
        /// <param name="request">update room command</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of type <see cref="Room"/></returns>
        public async Task<Room> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var property = await _context
                .Properties
                .Include(x => x.Rooms)
                .FirstOrDefaultAsync(x => x.Id == request.Id , cancellationToken);

            if (property == null)
            {
                throw new ProductException(ProductException.NOT_FOUND, $"property with Id {request.Id} not found");
            }

            var room = property.GetRoom(request.RoomId);
            room.UpdateRoom(request.AccommodationType, request.NrOfOccupants);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<Room>(room);
        }
    }
}
