namespace Availability.API.Features
{
    using AutoMapper;
    using CreateRoom;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using UpdatePrices;
    using Output = Models.Output;

    /// <summary>
    /// Entities controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IMongoDatabase _database;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">mediator instance</param>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="database"></param>
        public PropertiesController(IMediator mediator, IMapper mapper, IMongoDatabase database)
        {
            _mediator = mediator;
            _mapper = mapper;
            _database = database;
        }

        /// <summary>
        /// Creates a new <see cref="Output.Room"/>.
        /// </summary>
        /// <param name="id">Property Id</param>
        /// <param name="room">room details</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of the created <see cref="Output.Room"/></returns>
        [HttpPost("{id}/room")]
        [ProducesResponseType(typeof(Output.Room), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRoom(int id, Models.Input.Room.Create.CreateRoom room,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateCommand>(room);
            command.PropertyId = id;

            var newRoom = await _mediator.Send(command, cancellationToken);

            return Created($"room/{newRoom}", newRoom);
        }

        /// <summary>
        /// Updates a <see cref="Output.Room"/> price for a given date range.
        /// </summary>
        /// <param name="id">Property id</param>
        /// <param name="roomId">Room id</param>
        /// <param name="prices">room prices</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of the updated <see cref="Output.Room"/> with updated prices</returns>
        [HttpPut("{id}/room/{roomId}/prices")]
        [ProducesResponseType(typeof(Output.Room), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output.Room), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangePrice(int id, int roomId, IEnumerable<Models.Input.Room.Common.Price> prices,
            CancellationToken cancellationToken)
        {
            var command = new UpdatePricesCommand() { PropertyId = id, RoomId = roomId, Prices = prices };

            var updatedRoom = await _mediator.Send(command, cancellationToken);

            return Ok(updatedRoom);
        }
    }
}
