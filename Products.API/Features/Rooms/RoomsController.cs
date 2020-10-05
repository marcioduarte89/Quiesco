namespace Products.API.Features
{
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models.Input.Room;
    using System.Threading;
    using System.Threading.Tasks;
    using Rooms.Create;
    using Output = Models.Output;
    using UpdateRoom = Models.Input.Property.UpdateRoom;


    [ApiController]
    [Route("property")]
    public class RoomsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RoomsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("{id}/room")]
        [ProducesResponseType(typeof(Output.Property), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddPropertyRoom(int id, CreateRoom room, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateCommand>(room);
            var newProperty = await _mediator.Send(command, cancellationToken);

            return Ok(newProperty);
        }

        //[HttpPut("{propertyId}/room/{id}")]
        //[ProducesResponseType(typeof(Output.Property), StatusCodes.Status201Created)]
        //public async Task<IActionResult> UpdateProperty(int propertyId, int roomId, UpdateRoom property, CancellationToken cancellationToken)
        //{
        //    var command = _mapper.Map<UpdateCommand>(property);
        //    command.Id = id;
        //    var newProperty = await _mediator.Send(command, cancellationToken);

        //    return Ok(newProperty);
        //}
    }
}
