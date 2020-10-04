namespace Products.API.Features.Create
{
    using AutoMapper;
    using Core.Models;
    using MediatR;
    using Products.Infrastructure.Data;
    using System.Threading;
    using System.Threading.Tasks;
    using Output = Models.Output;

    public class CreateCommandHandler : IRequestHandler<CreateCommand, Output.Property>
    {
        private readonly ProductsContext _context;
        private readonly IMapper _mapper;

        public CreateCommandHandler(ProductsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Output.Property> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var property = new Property(request.Type, request.Name);
            _context.Properties.Add(property);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<Output.Property>(property);
        }
    }
}
