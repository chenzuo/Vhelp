using System;
using System.Configuration;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using VideoHelp.Infrastructure.Installers;
using VideoHelp.ReadModel.Infrastructure.Installers;
using VideoHelp.UI.Domain.LoginzaAuthentication;
using VideoHelp.UI.Domain.LoginzaAuthentication.ExtractStrategy;
using System.Linq;
using VideoHelp.UI.Web.AppStart;
using VideoHelp.UI.Web.AppStart.Installers;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Bootstrapper), "Start")]
[assembly: WebActivator.ApplicationShutdownMethod(typeof(Bootstrapper), "Stop")]

namespace VideoHelp.UI.Web.AppStart 
{
	public static class Bootstrapper {
	    private static IWindsorContainer _container;

	    public static void Start()
	    {
            _container = new WindsorContainer()
                .Install(
                    new TransportBusInstaller(new Uri(ConfigurationManager.AppSettings["EndpointUri"])),
                    new CommandBusInstaller(),
                    new RavenInstaller(),
                    new ReadRepositoryInstaller(),
                    new SecurityConfigurationInstaller(),
                    new NotificationBusInstaller()
                );

            _container.Register(Classes.FromThisAssembly()
                            .BasedOn<IController>()
                            .LifestyleTransient());

            _container.Register(Component.For<IWindsorContainer>().Instance(_container));

            DependencyResolver.SetResolver(_container.Resolve, type => _container.ResolveAll(type).OfType<object>());

		    var profileInfoExtractor = new AccountInformationExtractor(
                                                            new GoogleStratagy(), 
                                                            new YandexStratagy(),
                                                            new MailRuStratagy(),
                                                            new VkStratagy());

            _container.Register(Component.For<AccountInformationExtractor>().Instance(profileInfoExtractor));
		}

        public static void Stop()
        {
            _container.Dispose();
        }
	}
}
 

