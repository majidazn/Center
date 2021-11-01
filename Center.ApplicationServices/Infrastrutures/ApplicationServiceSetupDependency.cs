using Autofac;
using Center.ApplicationServices.Center.Commands.CreateCenterCommand;
using Center.ApplicationServices.Center.DomainServices;
using Center.ApplicationServices.CenterVariable.DomainServices;
using Center.Domain.CenterAggregate.DomainServices;
using Center.Domain.CenterVariableAggregate.DomainServices;
using FluentValidation;
using Framework.Behaviors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Center.ApplicationServices.Infrastrutures
{
   public class ApplicationServiceSetupDependency : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            #region MediatoR
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();

            // request & notification handlers
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });


            builder.RegisterAssemblyTypes(typeof(CreateCenterCommand).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IRequestHandler<,>));


            // Register the Command's Validators (Validators based on FluentValidation library)
            builder
                .RegisterAssemblyTypes(typeof(CreateCenterCommandValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();


            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(TransactionBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            //builder
            //    .RegisterAssemblyTypes(typeof(IRequest<>).Assembly)
            //    .Where(t => t.IsClosedTypeOf(typeof(IRequest<>)))
            //    .AsImplementedInterfaces().InstancePerLifetimeScope();

            //builder
            //    .RegisterAssemblyTypes(typeof(IRequestHandler<>).Assembly)
            //    .Where(t => t.IsClosedTypeOf(typeof(IRequestHandler<>)))
            //    .AsImplementedInterfaces().InstancePerLifetimeScope();
            #endregion

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => x.Namespace.StartsWith("Center.ApplicationServices.CommonServices"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<CenterDomainServices>().As<ICenterDomainServices>().InstancePerLifetimeScope();
            builder.RegisterType<CenterVariableDomainServices>().As<ICenterVariableDomainServices>().InstancePerLifetimeScope();
        }
    }
}
