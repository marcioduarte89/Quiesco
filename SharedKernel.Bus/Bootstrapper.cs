﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Container;
using NServiceBus.Settings;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SharedKernel.Bus
{
    public class Bootstrapper
    {
        public const string AUDIT_QUEUE = "audit";
        public const string ERROR_QUEUE = "error";
        public const string SCHEMA = "dbo"; // change this later

        private readonly IContainer container;

        protected TransportExtensions<SqlServerTransport> Transport;
        protected PersistenceExtensions<SqlPersistence> Persistence;
        public EndpointConfiguration EndpointConfiguration;

        // First simplified version of the boostrapper
        public Bootstrapper(string endpointName, string connectionString, IContainer container = null)
        {
            this.container = container;
            InitialiseEndpointConfiguration(endpointName);
            InitialiseTransport(connectionString);
            InitialisePersistency(connectionString);
            //RegisterDependencies();
            DefineConventions();
        }

        private void RegisterDependencies()
        {
            // Update this later
            EndpointConfiguration.UseContainer<AutofacBuilder>(customizations => {
                customizations.ExistingLifetimeScope(container);
            });
        }

        public Bootstrapper(string endpointName, string connectionString, IEnumerable<Route> routing, IContainer container = null) :
            this(endpointName, connectionString, container)
        {
            InitialiseRouting(routing);
        }

        /// <summary>
        /// Starts the endpoint
        /// </summary>
        /// <returns></returns>
        public async Task<IEndpointInstance> Start()
        {
            return await Endpoint.Start(EndpointConfiguration);
        }

        

        /// <summary>
        /// Initialises the default endpoint configuration
        /// </summary>
        protected virtual void InitialiseEndpointConfiguration(string name)
        {
            EndpointConfiguration = new EndpointConfiguration(name);

            EndpointConfiguration.AuditProcessedMessagesTo(AUDIT_QUEUE);
            EndpointConfiguration.SendFailedMessagesTo(ERROR_QUEUE);
            EndpointConfiguration.EnableInstallers();
        }

        /// <summary>
        /// Initialises the transport
        /// </summary>
        /// <returns>Returns an instance of the Transport</returns>
        protected virtual void InitialiseTransport(string connectionString)
        {
            // message doesn't have to be dispatched immediately after sending, it can be delivered at a later time if necessary.  
            //SQL server already have native delayed delivery so we can disable timout manager
            Transport = EndpointConfiguration.UseTransport<SqlServerTransport>();
            Transport.ConnectionString(connectionString)
                .DefaultSchema(SCHEMA) // change this in the future
                .NativeDelayedDelivery();

            Transport
                .SubscriptionSettings()
                .CacheSubscriptionInformationFor(TimeSpan.FromMinutes(1));

            Transport.UseSchemaForQueue(ERROR_QUEUE, SCHEMA);
            Transport.UseSchemaForQueue(AUDIT_QUEUE, SCHEMA);
        }

        /// <summary>
        /// Initialises Persistency
        /// </summary>
        protected virtual void InitialisePersistency(string connectionString)
        {
            Persistence = EndpointConfiguration.UsePersistence<SqlPersistence>();
            var dialect = Persistence.SqlDialect<SqlDialect.MsSqlServer>();
            dialect.Schema(SCHEMA);
            Persistence.ConnectionBuilder(connectionBuilder: () =>
            {
                return new SqlConnection(connectionString);
            });

            var subscriptions = Persistence.SubscriptionSettings();

            // Subscription information can be cached for a given period of time so that it does not have to be loaded every single time an event is being published
            // In systems where events are subscribed and unsubscribed regularly (e.g. desktop applications unsubscribe when shutting down) 
            // it makes sense to keep the caching period short or to disable the caching altogether:
            subscriptions.CacheFor(TimeSpan.FromMinutes(1));
        }

        /// <summary>
        /// Initialises transport routing
        /// </summary>
        /// <param name="routing">routing settings</param>
        public virtual void InitialiseRouting(IEnumerable<Route> routing)
        {
            if (routing != null && routing.Any())
            {
                var transportRouting = Transport.Routing();
                foreach (var endpointRoute in routing)
                {
                    transportRouting.RouteToEndpoint(Type.GetType(endpointRoute.Type), endpointRoute.EndpointName);
                }
            }
        }

        /// <summary>
        /// Convention definitions
        /// </summary>
        protected void DefineConventions()
        {
            var conventions = EndpointConfiguration.Conventions();
            conventions.DefiningCommandsAs(type => type.Namespace == "SharedKernel.Messages.Commands");
            conventions.DefiningEventsAs(type => type.Namespace == "SharedKernel.Messages.Events");
            conventions.DefiningMessagesAs(type => type.Namespace == "SharedKernel.Messages");
        }
    }
}