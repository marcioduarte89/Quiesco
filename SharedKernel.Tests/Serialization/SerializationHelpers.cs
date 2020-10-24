namespace SharedKernel.Tests.Serialization
{
    using System.IO;
    using Newtonsoft.Json;

    public class SerializationHelpers {

        public static TEntity GetEntityFromStream<TEntity>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
            {
                return default(TEntity);
            }

            using (var sr = new StreamReader(stream))
            {
                using (var jtr = new JsonTextReader(sr))
                {
                    var js = new JsonSerializer();
                    var searchResult = js.Deserialize<TEntity>(jtr);
                    return searchResult;
                }
            }
        }
    }
}
