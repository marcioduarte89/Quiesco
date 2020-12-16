namespace Reservation.API.Features.Properties
{
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using NServiceBus;
    using Reservation.API.Models.Input.Reservation.Create;
    using SharedKernel.Messages.Commands;
    using SharedKernel.Messages.Common;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Reservation controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IMessageSession stuff;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator">mediator instance</param>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="stuff"></param>
        public ReservationController(IMediator mediator, IMapper mapper, IMessageSession stuff)
        {
            _mediator = mediator;
            _mapper = mapper;
            this.stuff = stuff;
        }

        /// <summary>
        /// Creates a new reservation for a room
        /// </summary>
        /// <param name="reservation">Create reservation data</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateReservation(CreateReservation reservation, CancellationToken cancellationToken)
        {
            var startSaga = _mapper.Map<StartReservation>(reservation);
            startSaga.ReservationId = Guid.NewGuid();

            await stuff.SendLocal(startSaga);

            // Add uri here later
            return Created("", new
            {
                ReservationId = startSaga.ReservationId
            });
        }
    }
}
