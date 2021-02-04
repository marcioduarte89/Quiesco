namespace SharedKernel.Mongo.Indexes
{
    using MongoDB.Driver;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Index configurations
    /// </summary>
    public class IndexConfiguration
    {
        public static async Task CompoundIndexCreation<T>(string connectionString,
            string database,
            string collectioName,
            CreateIndexModel<T> indexModel,
            string indexName)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(database);

            var roomsIndexKeyBuilder = Builders<T>.IndexKeys;

            var existingIndexes = await (await db.GetCollection<T>(collectioName).Indexes.ListAsync()).ToListAsync();
            bool hasCompoundIndex = false;

            foreach (var index in existingIndexes.Where(index => index.Values.Any(x => x.Equals(indexName))))
            {
                hasCompoundIndex = true;
            }

            if (!hasCompoundIndex)
            {
                await db.GetCollection<T>(collectioName).Indexes.CreateOneAsync(indexModel);
            }
        }
    }
}
