namespace Availability.Infrastructure.Data.Repositories
{
    using Availability.Core.Models;
    using Nest;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Elastic search repository
    /// </summary>
    public class ElasticSearchRepository : IGlobalReadRepository
    {
        private readonly IElasticClient _elasticClient;

        public ElasticSearchRepository(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        /// <summary>
        /// Checks if the Rooms exists in the global elastic index
        /// </summary>
        /// <param name="propertyId">property id</param>
        /// <param name="roomId">room id</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns whether the room exists globally</returns>
        public async Task<bool> HasRoom(int propertyId, int roomId, CancellationToken cancellationToken)
        {
            var room = await _elasticClient.SearchAsync<Room>(x =>
                x.Query(q =>
                    q.Bool(b =>
                        b.Must(
                            m => m.Term(t1 => t1.PropertyId, propertyId),
                            m => m.Term(t1 => t1.RoomId, roomId)
                        )
                    )
                )
            );

            return room.Total > 0;
        }
    }
}
