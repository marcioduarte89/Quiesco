namespace Products.API.Features.Properties
{
    using AutoMapper;
    using CreateProperty;
    using CreateRoom;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Products.API.Features.Properties.UpdateProperty;
    using System.Threading;
    using System.Threading.Tasks;
    using UpdateRoom;
    using Output = Models.Output;

    /// <summary>
    /// Property controller - Aggregate Room for properties and Rooms
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">mediator instance</param>
        /// <param name="mapper">Mapper instance</param>
        public PropertiesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new <see cref="Output.Property"/>.
        /// </summary>
        /// <param name="property">Property details</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of the created <see cref="Output.Property"/></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(Output.Property), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProperty(Models.Input.Property.Create.CreateProperty property,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreatePropertyCommand>(property);
            var newProperty = await _mediator.Send(command, cancellationToken);

            return Created($"properties/{newProperty.Id}", newProperty);
        }

        /// <summary>
        /// Updates a <see cref="Output.Property"/>.
        /// </summary>
        /// <param name="id">Property id</param>
        /// <param name="property">Property details</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of the updated <see cref="Output.Property"/></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Output.Property), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Output.Property), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProperty(int id, Models.Input.Property.Update.UpdateProperty property,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdatePropertyCommand>(property);
            command.Id = id;
            var newProperty = await _mediator.Send(command, cancellationToken);

            return Ok(newProperty);
        }

        /// <summary>
        /// Adds a new <see cref="Output.Room"/> to a property
        /// </summary>
        /// <param name="id">Property Id</param>
        /// <param name="room">Room details</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of the created <see cref="Output.Room"/></returns>
        [HttpPost("{id}/room")]
        [ProducesResponseType(typeof(Output.Room), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Output.Room), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddPropertyRoom(int id, Models.Input.Room.Create.CreateRoom room,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateRoomCommand>(room);
            command.PropertyId = id;
            var newProperty = await _mediator.Send(command, cancellationToken);

            return Created($"properties/{id}/room/{newProperty.RoomId}", newProperty);
        }

        /// <summary>
        /// Updates a <see cref="Output.Room"/> in an existing property
        /// </summary>
        /// <param name="id">Property Id</param>
        /// <param name="roomId">Room id</param>
        /// <param name="room">Room details</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of the updated <see cref="Output.Room"/></returns>
        [HttpPut("{id}/room/{roomId}")]
        [ProducesResponseType(typeof(Output.Property), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Output.Property), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePropertyRoom(int id, int roomId, Models.Input.Room.Update.UpdateRoom room,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateRoomCommand>(room);
            command.Id = id;
            command.RoomId = roomId;
            var updatedRoom = await _mediator.Send(command, cancellationToken);

            return Ok(updatedRoom);
        }
    }
}
