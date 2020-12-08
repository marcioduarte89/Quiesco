namespace Reservation.API.Features.Properties
{
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using NServiceBus;
    using SharedKernel.Messages.Commands;
    using SharedKernel.Messages.Common;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Property controller - Aggregate Room for properties and Rooms
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IEndpointInstance stuff;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">mediator instance</param>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="stuff"></param>
        public PropertiesController(IMediator mediator, IMapper mapper, IEndpointInstance stuff)
        {
            _mediator = mediator;
            _mapper = mapper;
            this.stuff = stuff;
        }

        /// <summary>
        /// Creates a new <see cref="Output.Property"/>.
        /// </summary>
        /// <param name="property">Property details</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns an instance of the created <see cref="Output.Property"/></returns>
        //[HttpPost("")]
        //[ProducesResponseType(typeof(Output.Property), StatusCodes.Status201Created)]
        //public async Task<IActionResult> CreateProperty(Models.Input.Property.Create.CreateProperty property,
        //    CancellationToken cancellationToken)
        //{
        //    var command = _mapper.Map<CreatePropertyCommand>(property);
        //    var newProperty = await _mediator.Send(command, cancellationToken);

        //    return Created($"properties/{newProperty.Id}", newProperty);
        //}
        [HttpPost("")]
        public async Task<IActionResult> CreateProperty()
        {

            //await stuff.Send(new Example() { Id = 1 });

            var dayIn = 1;
            var dayOut = 1;
            var monthIn = 1;
            var monthOut = 1;
            var year = 2020;

            var startDate = new DateTime(year, monthIn, dayIn);
            var endDate = new DateTime(year, monthOut, dayOut);

            await stuff.SendLocal(new StartReservation() 
            { 
                ReservationId = Guid.NewGuid(),
                CheckIn = startDate,
                CheckOut = endDate,
                NumberOfOccupants = 2,
                Price = 200,
                PropertyId = 1,
                RoomId = 1,
                User = new User()
            });

            return Ok(null);
        }
    }
}
