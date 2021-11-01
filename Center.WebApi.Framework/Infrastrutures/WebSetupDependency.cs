using Autofac;
using Center.DataAccess.Infrastrutures;
using Framework.DynamicPermissions;
using Framework.DynamicPermissions.Services.HashingService;
using Framework.DynamicPermissions.Services.MvcActionsDiscovery;
using Framework.DynamicPermissions.Services.SecurityTrimming;
using Framework.Security;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.WebApi.Framework.Infrastrutures
{
    public class WebSetupDependency : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterAssemblyModules(typeof(DataAccessSetupDependency).Assembly);

            builder.RegisterType<TokenValidatorService>().As<ITokenValidatorService>().InstancePerLifetimeScope();

            #region DynamicPermissionsAuthorization

            builder.RegisterType<DynamicPermissionsAuthorizationHandler>().As<IAuthorizationHandler>().InstancePerLifetimeScope();
            builder.RegisterType<SecurityTrimmingService>().As<ISecurityTrimmingService>().InstancePerLifetimeScope();
            builder.RegisterType<TokenProvider>().As<ITokenProvider>().InstancePerLifetimeScope();

            builder.RegisterType<MvcActionsDiscoveryService>().As<IMvcActionsDiscoveryService>().SingleInstance();
            builder.RegisterType<HashingService>().As<IHashingService>().InstancePerDependency();

            #endregion
        }
    }
}
