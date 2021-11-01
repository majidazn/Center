using Autofac;
using Center.ApplicationServices.Infrastrutures;
using Center.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace Center.DataAccess.Infrastrutures
{
    public class DataAccessSetupDependency : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterAssemblyModules(typeof(ApplicationServiceSetupDependency).Assembly);

            builder.RegisterType<CenterBoundedContextCommand>().As<DbContext>().InstancePerLifetimeScope();
            builder.RegisterType<CenterBoundedContextQuery>().InstancePerLifetimeScope();


            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                 .Where(x => x.Namespace.StartsWith("Center.DataAccess.Repositories"))
                 .AsImplementedInterfaces()
                 .InstancePerLifetimeScope();


        }
    }
}
