namespace Availability.API.Infrastructure.Ioc 
{

    using Autofac;
    using Availability.API.Infrastructure.Behaviours;
    using MediatR;
    using System;
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

            RegisterBehaviors(builder);
        }

        /// <summary>
        /// Register mediator behaviours
        /// </summary>
        /// <param name="builder"></param>
        private void RegisterBehaviors(ContainerBuilder builder)
        {
            var behaviors = new Type[] {
                typeof(VerifyPropertyGloballyBehaviour<,>)
            };

            foreach (var behavior in behaviors)
            {
                builder
                   .RegisterGeneric(behavior)
                   .As(typeof(IPipelineBehavior<,>))
                   .InstancePerLifetimeScope();
            }
        }
    }
}
