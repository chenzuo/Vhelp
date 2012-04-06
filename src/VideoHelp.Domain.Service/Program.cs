using System;
using System.Configuration;
using System.Threading;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Topshelf;
using VideoHelp.Infrastructure.Installers;

namespace VideoHelp.Domain.Service
{
    class Program
    {
        private IWindsorContainer _container;

        static void Main()
        {
            Thread.CurrentThread.Name = "Domain Service Main Thread";
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

                x.SetDescription("Handles the domain logic for the VideoHelp Application.");
                x.SetDisplayName("VideoHelp domain service");
                x.SetServiceName("VideoHelp.Domain.Service");
            });
        }

        private void start()
        {
            NLog.Config.SimpleConfigurator.ConfigureForConsoleLogging();

            _container = new WindsorContainer()
                .Install(
                    new TransportBusInstaller(new Uri(ConfigurationManager.AppSettings["EndpointUri"])),
                    new CommandBusInstaller(),
                    new EventBusInstaller(),
                    new EventStoreInstaller(),
                    new CommandHandlerInstaller()
                    );

            _container.Register(Component.For<IWindsorContainer>().Instance(_container));
        }

        private void stop()
        {
            _container.Dispose();
        }
    }
}
