using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentSecurity;
using VideoHelp.UI.Web.Controllers;

namespace VideoHelp.UI.Web.AppStart.Installers
{
    public class SecurityConfigurationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            SecurityConfigurator.Configure(configuration =>
                                               {
                                                   configuration.ResolveServicesUsing(type => container.ResolveAll(type).OfType<object>());

                                                   // Let Fluent Security know how to get the authentication status of the current user
                                                   configuration.GetAuthenticationStatusFrom(() => UserManager.IsUserLogged);

                                                   configuration.ForAllControllers().DenyAnonymousAccess();

                                                   // This is where you set up the policies you want Fluent Security to enforce on your controllers and actions
                                                   configuration.For<HomeController>(x => x.Index()).Ignore();
                                                   configuration.For<HomeController>(x => x.About()).Ignore();
                                                   //configuration.For<HomeController>(x => x.Test()).DenyAuthenticatedAccess();

                                                   configuration.For<AuthenticationController>().Ignore();
                                                   configuration.For<AuthenticationController>(x => x.Logoff()).DenyAnonymousAccess();
                                               });
        }
    }
}