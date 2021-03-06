﻿namespace Availability.Infrastructure.Data.Repositories
{
    using Common.Exceptions;
    using Core.Models;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Repository Base
    /// </summary>
    public abstract class RepositoryBase
    {
        private readonly IMongoDatabase _mongoDatabase;

        private readonly IReadOnlyDictionary<Type, string> _collectionTypeToNameMap =
            new Dictionary<Type, string>() {{typeof(Room), "rooms"}};

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
                throw new AvailabilityException(AvailabilityException.INVALID_DATA,
                    $"Collection type {nameof(TEntity)} not found");
            }

            return _mongoDatabase.GetCollection<TEntity>(name);
        }
    }
}
