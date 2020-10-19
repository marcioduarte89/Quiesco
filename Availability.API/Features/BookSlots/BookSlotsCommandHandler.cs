namespace Availability.API.Features.BookSlots
{
    using AutoMapper;
    using MediatR;
    using Models.Output;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Command handler to book room slots
    /// </summary>
    public class BookSlotsCommandHandler : IRequestHandler<BookSlotsCommand, Room>
    {
        private IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper">Mapper instance</param>
        public BookSlotsCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Handles commands of type BookSlotsCommand
        /// </summary>
        /// <param name="request">Book slots command</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of type <see cref="Room"/></returns>
        public Task<Room> Handle(BookSlotsCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
