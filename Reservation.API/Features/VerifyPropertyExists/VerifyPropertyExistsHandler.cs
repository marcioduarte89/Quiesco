namespace Reservation.API.Features.CreateReservation
{
    using Nest;
    using NServiceBus;
    using Reservations.Core.Models;
    using SharedKernel.Messages.Commands.Reservation;
    using SharedKernel.Messages.Events.Reservation;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates a new Reservation
    /// </summary>
    public class VerifyPropertyExistsHandler : IHandleMessages<VerifyPropertyExists>
    {
        private readonly IElasticClient _elasticClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="elasticClient">Elastic client</param>
        public VerifyPropertyExistsHandler(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        /// <summary>
        /// Handles the creation of a new reservation
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Handle(VerifyPropertyExists message, IMessageHandlerContext context)
        {
            var room = await _elasticClient.SearchAsync<Room>(x =>
                x.Query(q =>
                    q.Bool(b =>
                        b.Must(
                            m => m.Term(t1 => t1.PropertyId, message.PropertyId),
                            m => m.Term(t1 => t1.RoomId, message.RoomId)
                        )
                    )
                )
            );

            await context.Publish(new PropertyExistanceVerified()
            {
                ReservationId = message.ReservationId,
                PropertyId = message.PropertyId,
                RoomId = message.RoomId,
                Exists = room?.Total > 0
            });
        }
    }
}
