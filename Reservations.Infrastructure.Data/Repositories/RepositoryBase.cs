namespace Reservations.Infrastructure.Data.Repositories
{
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using Reservations.Core.Models;
    using Reservation.Common.Exceptions;

    /// <summary>
    /// Repository Base
    /// </summary>
    public abstract class RepositoryBase
    {
        private readonly IMongoDatabase _mongoDatabase;

        private readonly IReadOnlyDictionary<Type, string> _collectionTypeToNameMap =
            new Dictionary<Type, string>() {{typeof(Reservation), "reservations"}};

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mongoDatabase"></param>
        protected RepositoryBase(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        /// <summary>
        /// Gets the collection based on the type provided
        /// </summary>
        /// <typeparam name="TEntity">Collection type</typeparam>
        /// <returns>Returns an instance of <see cref="IMongoCollection{TEntity}"/></returns>
        /// <exception cref="AvailabilityException">If type provided is not supported</exception>
        protected IMongoCollection<TEntity> GetTypeCollection<TEntity>()
        {
            if (!_collectionTypeToNameMap.TryGetValue(typeof(TEntity), out var name))
            {
                throw new ReservationException(ReservationException.INVALID_DATA,
                    $"Collection type {nameof(TEntity)} not found");
            }

            return _mongoDatabase.GetCollection<TEntity>(name);
        }
    }
}
