namespace SharedKerner.Mongo
{
    using MongoDB.Driver;
    using SharedKernel.Mongo.ClassMaps;
    using SharedKernel.Mongo.Conventions;
    using SharedKernel.Mongo.Indexes;
    using SharedKernel.Mongo.Serializers;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Configures mongo instance
    /// </summary>
    public class MongoConfiguration
    {
        private readonly string _connectionString;
        private readonly string _database;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="database">Database name</param>
        public MongoConfiguration(string connectionString, string database)
        {
            _connectionString = connectionString;
            _database = database;
        }

        public virtual MongoConfiguration RegisterGlobalSerializers()
        {
            GlobalSerializersRegistration.Load();
            return this;
        }

        public virtual MongoConfiguration RegisterConventions()
        {
            ConventionRegistration.Load();
            return this;
        }

        public virtual MongoConfiguration RegisterClassMaps(IEnumerable<Type> classMapRegistrations)
        {
            foreach (var classMap in classMapRegistrations)
            {
                var instantiatedTypes = Activator.CreateInstance(classMap) as IClassMapRegistration;

                if(instantiatedTypes == null)
                {
                    throw new ArgumentException($"Class map registration provided  {classMap.AssemblyQualifiedName} is not of type {nameof(IClassMapRegistration)}");
                }

                instantiatedTypes.Load();
            }
            
            return this;
        }

        public virtual async Task<MongoConfiguration> ConfigureCoumpoundIndexes<T>(
            string collectionName,
            CreateIndexModel<T> indexModel,
            string indexName)
        {
            await IndexConfiguration.CompoundIndexCreation(_connectionString, _database, collectionName, indexModel, indexName);
            return this;
        }
    }
}
