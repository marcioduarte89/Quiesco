namespace Availability.API.IntegrationTests.Mocks
{
    using Availability.Infrastructure.Data.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Global mock repository
    /// </summary>
    public class GlobalMockRepository : IGlobalReadRepository
    {
        /// <summary>
        /// Mocks a globally available room
        /// </summary>
        /// <param name="propertyId">property id</param>
        /// <param name="roomId">room id</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Always returns true</returns>
        public Task<bool> HasRoom(int propertyId, int roomId, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
