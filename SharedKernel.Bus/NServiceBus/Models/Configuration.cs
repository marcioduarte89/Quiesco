namespace SharedKernel.Bus.NServiceBus.Models
{
    using System;
    using System.Collections.Generic;

    public class Configuration
    {
        public IEnumerable<Route> Routes { get; set; }

        public string EndpointName { get; set; }
    }
}
