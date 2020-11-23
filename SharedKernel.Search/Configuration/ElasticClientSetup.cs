using Microsoft.Extensions.Configuration;
using Nest;
using SharedKernel.Search.Models;
using System;
using System.Collections.Generic;

namespace SharedKernel.Search.Configuration
{
    public class ElasticClientSetup
    {
        private readonly SearchConfiguration searchConfig;
        private ConnectionSettings _connectionSettings;

        private ElasticClientSetup(SearchConfiguration searchConfig)
        {
            this.searchConfig = searchConfig;
        }

        public static ElasticClientSetup Initialise(SearchConfiguration searchConfig)
        {
            return new ElasticClientSetup(searchConfig);
        }

        public ElasticClientSetup ConnectionsSettingsSetup()
        {
            if(searchConfig.EnvironmentName.Equals("Development", StringComparison.OrdinalIgnoreCase))
            {
                _connectionSettings = new ConnectionSettings(new Uri(searchConfig.Uri))
                    .EnableDebugMode();
            }
            
            return this;
        }

        public ElasticClientSetup ConnectionsSettingsSetup(ConnectionSettings connectionSettings)
        {
            _connectionSettings = connectionSettings;
            return this;
        }

        public ElasticClientSetup AddClientMappings<TDocument>(Func<ClrTypeMappingDescriptor<TDocument>, IClrTypeMapping<TDocument>> selector) where TDocument : class
        {
            _connectionSettings.DefaultMappingFor(selector);
            return this;
        }

        public ElasticClientSetup RegisterOnRequestComplete()
        {
            if(_connectionSettings == null)
            {
                throw new InvalidOperationException("Cannot register on request complete without having setup Connection Settings");
            }

            _connectionSettings.OnRequestCompleted(x =>
            {
                
                // do stuff here
            });
            return this;
        }

        public ElasticClientSetup RegisterOnRequestComplete(Action handler)
        {
            if (_connectionSettings == null)
            {
                throw new InvalidOperationException("Cannot register on request complete without having setup Connection Settings");
            }
            _connectionSettings.OnRequestCompleted(x =>
            {
                // do stuff here
                handler();
            });

            return this;
        }

        // Tweek client settings here for performance based on environment in this client

        public ElasticClient Create()
        {
            return new ElasticClient(_connectionSettings);
        }
    }
}
