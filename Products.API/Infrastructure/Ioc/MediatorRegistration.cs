namespace Products.API.Infrastructure.Ioc
{
    using Autofac;
    using MediatR;
    using System.Reflection;
    using Module = Autofac.Module;

    /// <summary>
    /// Registers Mediator related configuration
    /// </summary>
    public class MediatorRegistration : Module
    {
        /// <summary>
        /// Loads the container builder and registers mediator components
        /// </summary>
        /// <param name="builder">Container builder</param>
        protected override void Load(ContainerBuilder builder)
        {

            builder
                .RegisterAssemblyTypes(typeof(IMediator).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(ctx =>
            {

                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            var mediatrOpenTypes = new[] {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            }

        }
    }
}
