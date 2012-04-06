using System;
using System.Collections.Generic;
using FluentSecurity;
using VideoHelp.Controllers;
using VideoHelp.Helpers;

namespace VideoHelp
{
    public class SecurityConfig
    {

        public static void Setup(Func<Type, IEnumerable<object>> serviceLocator)
        {
            SecurityConfigurator.Configure(configuration =>
            {
                configuration.ResolveServicesUsing(serviceLocator);
                
                configuration.GetAuthenticationStatusFrom(SecurityHelper.UserIsAuthenticated);
                configuration.ForAllControllers().Ignore();

                configuration.For<HomeController>(x => x.Index()).Ignore();
                configuration.For<HomeController>(x => x.About()).DenyAnonymousAccess();
            });
        }
         
    }
}