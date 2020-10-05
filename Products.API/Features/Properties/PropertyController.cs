namespace Products.API.Features.Properties
{
    using AutoMapper;
    using Create;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models.Input.Property;
    using System.Threading;
    using System.Threading.Tasks;
    using Update;
    using Output = Models.Output;

    [ApiController]
    [Route("[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PropertyController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(Output.Property), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProperty(CreateProperty property, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateCommand>(property);
            var newProperty = await _mediator.Send(command, cancellationToken);

            return Ok(newProperty);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Output.Property), StatusCodes.Status201Created)]
        public async Task<IActionResult> UpdateProperty(int id, UpdateRoom property, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateCommand>(property);
            command.Id = id;
            var newProperty = await _mediator.Send(command, cancellationToken);

            return Ok(newProperty);
        }
    }
}
