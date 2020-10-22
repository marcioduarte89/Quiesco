using System;
using System.Threading.Tasks;

namespace Availability.API.IntegrationTests.Infrastructure.Containers
{
    using Docker.DotNet.Models;

    public class MongoContainer : DockerContainerBase
    {
        public MongoContainer(string imageName, string containerName) : base(imageName, containerName)
        {
        }

        protected override Task<bool> IsReady()
        {
            throw new NotImplementedException();
        }

        protected override HostConfig ToHostConfig()
        {
            throw new NotImplementedException();
        }

        protected override Config ToConfig()
        {
            throw new NotImplementedException();
        }
    }
}
