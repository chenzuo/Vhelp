using System;
using System.Configuration;
using System.Threading;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NLog;
using Topshelf;
using VideoHelp.Domain.Messages.Events.Users;
using VideoHelp.Infrastructure;
using VideoHelp.Infrastructure.Installers;

namespace VideoHelp.Domain.EventListener
{
    class Program
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private IWindsorContainer _container;

        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "DomainEventListener Main Thread";
            HostFactory.Run(x =>
            {
                x.Service<Program>(s =>
                {
                    s.ConstructUsing(name => new Program());
                    s.WhenStarted(p => p.start());
                    s.WhenStopped(p => p.stop());
                });

                x.UseNLog();
                x.RunAsLocalSystem();

                x.SetDescription("Listen the event from application bus.");
                x.SetDisplayName("VideoHelp domain event listner");
                x.SetServiceName("VideoHelp.DomainEventListener");
            });
        }

        private void stop()
        {
            _container.Dispose();
        }


        private void start()
        {
            _container = new WindsorContainer()
                .Install(
                    new TransportBusInstaller(new Uri(ConfigurationManager.AppSettings["EndpointUri"])),
                    new EventBusInstaller()
                    );

            _container.Register(Component.For<IWindsorContainer>().Instance(_container));

           
            var eventBus = _container.Resolve<IEventBus>();
            var i = 1;
            eventBus.RegisterEventHandler<UserCreated>(created => Console.WriteLine("{1}. User was created. Name {0}", created.FullName, i++));
            
        }
    }
}
