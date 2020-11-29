namespace SharedKernel.Search.Models
{
    using System;

    /// <summary>
    /// Search configuration
    /// </summary>
    public class SearchConfiguration
    {
        /// <summary>
        /// Environment Name
        /// </summary>
        public string EnvironmentName {get;set;}

        /// <summary>
        /// Elastic search client URI
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Service default Index
        /// </summary>
        public string DefaultIndex { get; set; }

        /// <summary>
        /// Enable debug features
        /// </summary>
        public bool EnableDebug { get; set; }
    }
}
