using System;
using System.Configuration;
using System.Threading;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Topshelf;
using VideoHelp.Infrastructure;
using VideoHelp.Infrastructure.Installers;
using VideoHelp.ReadModel.Infrastructure.Installers;

namespace VideoHelp.ReadModel.Service
{
    class Program
    {
        private IWindsorContainer _container;

        static void Main()
        {
            Thread.CurrentThread.Name = "Handle event service main thread";
            HostFactory.Run(x =>
            {
                x.Service<Program>(s =>
                {
                    s.ConstructUsing(name => new Program());
                    s.WhenStarted(p => p.start());
                    s.WhenStopped(p => p.stop());
                });
                x.RunAsLocalSystem();
                x.UseNLog();

                x.SetDescription("Handles the domain events for the VideoHelp.");
                x.SetDisplayName("VideoHelp handle event service");
                x.SetServiceName("VideoHelp.HandleEventService");
            });
        }

        private void start()
        {
            NLog.Config.SimpleConfigurator.ConfigureForConsoleLogging();

            _container = new WindsorContainer()
                .Install(
                    new TransportBusInstaller(new Uri(ConfigurationManager.AppSettings["EndpointUri"])),
                    new NotificationBusInstaller(),    
                    new EventBusInstaller(),
                    new RavenInstaller(),
                    new RavenIndexInstaller(),
                    new EventHandlersInstaller()                
                    );

            _container.Register(Component.For<IWindsorContainer>().Instance(_container));
        }

        private void stop()
        {
            _container.Dispose();
        }
    }
}