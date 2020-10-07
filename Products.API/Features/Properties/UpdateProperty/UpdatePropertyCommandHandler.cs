namespace Products.API.Features.Properties.UpdateProperty
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
    /// Update property command handler
    /// Handles the update of Properties within the Products API
    /// </summary>
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, Property>
    {
        private readonly ProductsContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Products Context</param>
        /// <param name="mapper">Mapper instance</param>
        public UpdatePropertyCommandHandler(ProductsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles commands of type UpdatePropertyCommand and updates Properties
        /// </summary>
        /// <param name="request">update property command</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of type <see cref="Property"/></returns>
        public async Task<Property> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
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
