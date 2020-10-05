namespace Products.API.Features.Rooms.Create
{
    using AutoMapper;
    using MediatR;
    using Products.Infrastructure.Data;
    using System.Threading;
    using System.Threading.Tasks;
    using Output = Models.Output;

    public class CreateCommandHandler : IRequestHandler<CreateCommand, Output.Room>
    {
        private readonly ProductsContext _context;
        private readonly IMapper _mapper;

        public CreateCommandHandler(ProductsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Output.Room> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
