namespace Availability.API.Infrastructure.Behaviours
{
    using Availability.API.Infrastructure.Interfaces;
    using Availability.Common.Exceptions;
    using Availability.Infrastructure.Data.Repositories;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Verifies a property is available and created globally before allowing it setting it up
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class VerifyPropertyGloballyBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IShouldVerifyPropertyGlobally
    {
        private readonly Func<IGlobalReadRepository> _globalRepositoryFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        public VerifyPropertyGloballyBehaviour(Func<IGlobalReadRepository> globalRepositoryFactory)
        {
            _globalRepositoryFactory = globalRepositoryFactory;
        }

        /// <summary>
        /// Checks if a property exists locally - If not, an error will be thrown
        /// </summary>
        /// <param name="request">Current request</param>
        /// <param name="cancellationToken"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var repository = _globalRepositoryFactory();
            var hasRoom = await repository.HasRoom(request.PropertyId, request.RoomId, cancellationToken);
            if (!hasRoom)
            {
                throw new AvailabilityException(AvailabilityException.INVALID_DATA, $"The room with id {request.RoomId} in property id {request.PropertyId} does not exist");
            }

            return await next();
        }
    }
}
