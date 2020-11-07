//namespace Reservation.API.Features.Properties.CreateProperty {
//    using AutoMapper;
//    using Core.Models;
//    using MediatR;
//    using Products.Infrastructure.Data;
//    using System.Threading;
//    using System.Threading.Tasks;
//    using Output = Models.Output;

//    /// <summary>
//    /// Create property command handler
//    /// Handles the creation of Properties within the Products API
//    /// </summary>
//    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, Output.Property> {
//        private readonly ProductsContext _context;
//        private readonly IMapper _mapper;

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <param name="context">Products Context</param>
//        /// <param name="mapper">Mapper instance</param>
//        public CreatePropertyCommandHandler(ProductsContext context, IMapper mapper) {
//            _context = context;
//            _mapper = mapper;
//        }

//        /// <summary>
//        /// Handles commands of type CreatePropertyCommand and creates Properties
//        /// </summary>
//        /// <param name="request">Create property command</param>
//        /// <param name="cancellationToken"></param>
//        /// <returns>Returns an instance of type <see cref="Output.Property"/></returns>
//        public async Task<Output.Property> Handle(CreatePropertyCommand request, CancellationToken cancellationToken) {
//            var property = new Property(request.Type, request.Name);
//            _context.Properties.Add(property);
//            await _context.SaveChangesAsync(cancellationToken);

//            return _mapper.Map<Output.Property>(property);
//        }
//    }
//}
