using System;
using System.Configuration;
using System.Threading;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Topshelf;
using VideoHelp.Domain.Messages.Events.MediaContent;
using VideoHelp.Domain.Messages.Events.Meeting;
using VideoHelp.Domain.Messages.Events.Users;
using VideoHelp.Infrastructure;
using VideoHelp.Infrastructure.Installers;
using VideoHelp.ReadModel.Contracts;
using VideoHelp.ReadModel.Infrastructure.Installers;
using VideoHelp.ReadModel.Meeting;
using VideoHelp.ReadModel.Users;

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
                    new EventBusInstaller(),
                    new RavenInstaller(),
                    new ReadRepositoryInstaller(),
                    new WriteRepositoryInstaller(),
                    new NotificationBusInstaller()
                    );

            _container.Register(Component.For<IWindsorContainer>().Instance(_container));

            var eventBus = _container.Resolve<IEventBus>();

            var userEventHandler = new UserEventHandler(_container.Resolve<IWriteRepository>(), _container.Resolve<INotificationBus>());
            var meetingEventHandler = new MeetingEventHandler(_container.Resolve<IWriteRepository>(), _container.Resolve<IReadRepository>(), _container.Resolve<INotificationBus>());
            var meetingListEventHandler = new MeetingListEventHandler(_container.Resolve<IWriteRepository>(), _container.Resolve<IReadRepository>(), _container.Resolve<INotificationBus>());

            eventBus.RegisterEventHandler<UserCreated>(userEventHandler.Handle);
            eventBus.RegisterEventHandler<UserAssociatedWithIdentity>(userEventHandler.Handle);


            eventBus.RegisterEventHandler<MeetingCreated>(meetingEventHandler.Handle);
            eventBus.RegisterEventHandler<CameraStreamCreated>(meetingEventHandler.Handle);

            eventBus.RegisterEventHandler<MeetingCreated>(meetingListEventHandler.Handle);
        }

        private void stop()
        {
            _container.Dispose();
        }
    }
}