namespace Reservation.API.Features.StartReservation
{
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using NServiceBus;
    using SharedKernel.Messages.Commands.Reservation;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using InputModel = Reservation.API.Models.Input.Reservation.Create;

    /// <summary>
    /// Reservation controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMessageSession _session;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="session"></param>
        public ReservationController(IMapper mapper, IMessageSession session)
        {
            _mapper = mapper;
            _session = session;
        }

        /// <summary>
        /// Creates a new reservation for a room
        /// </summary>
        /// <param name="reservation">Create reservation data</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateReservation(InputModel.CreateReservation reservation, CancellationToken cancellationToken)
        {
            var startSaga = _mapper.Map<StartReservation>(reservation);
            startSaga.ReservationId = Guid.NewGuid();

            await _session.SendLocal(startSaga);

            // Add uri here later
            return Created("", new
            {
                startSaga.ReservationId
            });
        }
    }
}
