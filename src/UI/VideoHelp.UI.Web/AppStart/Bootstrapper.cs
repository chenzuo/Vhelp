using System;
using System.Configuration;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MassTransit;
using SignalR;
using SignalR.Hubs;
using VideoHelp.Infrastructure;
using VideoHelp.Infrastructure.Installers;
using VideoHelp.ReadModel.Contracts;
using VideoHelp.ReadModel.Infrastructure.Installers;
using VideoHelp.ReadModel.Meeting;
using VideoHelp.UI.Domain.LoginzaAuthentication;
using VideoHelp.UI.Domain.LoginzaAuthentication.ExtractStrategy;
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
	    private static IServiceBus _serviceBus;
	    private static ICommandBus _commandBus;
	    private static INotificationBus _notificationBus;

	    public static void Start()
	    {
	        var endpoint = new Uri(ConfigurationManager.AppSettings["EndpointUri"]);

	        _container = new WindsorContainer()
                .Install(
                    new TransportBusInstaller(endpoint),
                    new CommandBusInstaller(),
                    new RavenInstaller(),
                    new RavenRepositoryFactoryInstaller(),
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
            _serviceBus = _container.Resolve<IServiceBus>();
            _commandBus = _container.Resolve<ICommandBus>();
	        _notificationBus = _container.Resolve<INotificationBus>();

            Global.DependencyResolver.Register(typeof(IConnectionIdFactory), () => new UserConnectionIdFactory());

            var connectionManager = Global.DependencyResolver.GetService(typeof(IConnectionManager)) as IConnectionManager;

	        _notificationBus.SubscribeNotification<MeetingView>(guid =>
	                                                                {
	                                                                    var clients = connectionManager.GetClients<MeetingHub>();
                                                                        clients[guid.ToString()].test();
	                                                                });
	    }

        public static void Stop()
        {
            
            _serviceBus.Dispose();
            _container.Dispose();
        }
	}

}
 

