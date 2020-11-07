namespace Availability.API.Infrastructure.Ioc
{
    using Autofac;

    /// <summary>
    /// NServicebus components registration
    /// </summary>
    public class NServiceBusRegistration : Module
    {
        /// <summary>
        /// Loads the container builder and registers generic components
        /// </summary>
        /// <param name="builder">Container builder</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder
            .Register(ctx => NServiceBusBootstrapper.Endpoint)
            .AsImplementedInterfaces()
            .SingleInstance();
        }
    }
}
