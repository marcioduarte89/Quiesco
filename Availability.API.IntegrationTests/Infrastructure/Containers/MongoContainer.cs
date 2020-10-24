namespace Availability.API.IntegrationTests.Infrastructure.Containers
{
    using Docker.DotNet.Models;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MongoContainer : DockerContainerBase
    {
        public static readonly string ConnectionString = "mongodb://127.0.0.1:27017";

        public MongoContainer() : base("mongo:latest", $"mongo{ContainerPrefix}{Guid.NewGuid()}")
        {
        }

        protected override async Task<bool> IsReady()
        {
            try
            {
                var client = new MongoClient(ConnectionString);
                var dbNames = await (await client.ListDatabaseNamesAsync()).ToListAsync();

                if (dbNames != null && dbNames.Any())
                {
                    return true;
                }
            }
            catch
            {
                // ignored
            }

            return false;
        }

        protected override HostConfig ToHostConfig()
        {
            return new HostConfig
            {
                PortBindings = new Dictionary<string, IList<PortBinding>>
                {
                    {
                        "27017/tcp",
                        new List<PortBinding>
                        {
                            new PortBinding
                            {
                                HostPort = "27017",
                                HostIP = "127.0.0.1"
                            }
                        }
                    }
                }
            };
        }

        protected override Config ToConfig()
        {
            return new Config();
        }
    }
}
