namespace Products.API.Features
{
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Create;
    using System.Threading;
    using System.Threading.Tasks;
    using Input = Models.Input;
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
        public async Task<IActionResult> CreateProperty(Input.Property property, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateCommand>(property);
            var newProperty = await _mediator.Send(command, cancellationToken);

            return Ok(newProperty);
        }
    }
}
