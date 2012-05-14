using System;
using System.Configuration;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using SignalR;
using VideoHelp.Infrastructure.Installers;
using VideoHelp.ReadModel.Infrastructure.Installers;
using System.Linq;
using VideoHelp.UI.Web.AppStart;
using VideoHelp.UI.Web.AppStart.Installers;
using VideoHelp.UI.Web.Hubs;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Bootstrapper), "Start")]
[assembly: WebActivator.ApplicationShutdownMethod(typeof(Bootstrapper), "Stop")]

namespace VideoHelp.UI.Web.AppStart 
{
    
	public static class Bootstrapper {
	    private static IWindsorContainer _container;

	    public static void Start()
	    {
	        var endpoint = new Uri(ConfigurationManager.AppSettings["EndpointUri"]);

	        _container = new WindsorContainer()
                .Install(
                    new TransportBusInstaller(endpoint),
                    new CommandBusInstaller(),
                    new NotificationBusInstaller(),
                    new RavenInstaller(),
                    new SecurityConfigurationInstaller(),
                    new ViewRepositoryInstaller()
                );

            _container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient());

            _container.Register(Component.For<IWindsorContainer>().Instance(_container));

            DependencyResolver.SetResolver(_container.Resolve, type => _container.ResolveAll(type).OfType<object>());
            Global.DependencyResolver.Register(typeof(IConnectionIdFactory), () => new UserConnectionIdFactory());

	    }

        public static void Stop()
        {
            _container.Dispose();
        }
	}

}
 

