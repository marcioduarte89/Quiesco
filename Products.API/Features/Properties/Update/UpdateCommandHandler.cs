namespace Products.API.Features.Properties.Update
{
    using MediatR;
    using Products.API.Models.Output;
    using Products.Infrastructure.Data;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Products.Common.Exceptions;

    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, Property>
    {
        private readonly ProductsContext _context;
        private readonly IMapper _mapper;

        public UpdateCommandHandler(ProductsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Property> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var property = await _context.Properties.FirstOrDefaultAsync(x => x.Id == request.Id , cancellationToken);

            if (property == null)
            {
                throw new ProductException(ProductException.NOT_FOUND, $"property with Id {request.Id} not found");
            }

            property.SetName(request.Name);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<Property>(property);
        }
    }
}
