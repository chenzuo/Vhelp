using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using VideoHelp.Domain.Messages.Events.MediaContent;
using VideoHelp.Domain.Messages.Events.Meeting;
using VideoHelp.Domain.Messages.Events.Users;
using VideoHelp.Infrastructure;

namespace VideoHelp.ReadModel.Infrastructure.Installers
{
    public class EventHandlersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromAssemblyContaining<ViewRepository>()
                                  .BasedOn(typeof(IEventHandler<>)).WithService.AllInterfaces());

            var eventBus = container.Resolve<IEventBus>();

            eventBus.RegisterEventHandler<CameraStreamCreated>(container.Resolve<IEventHandler<CameraStreamCreated>>().Handle);
            eventBus.RegisterEventHandler<MeetingCreated>(container.Resolve<IEventHandler<MeetingCreated>>().Handle);
            eventBus.RegisterEventHandler<UserAssociatedWithIdentity>(container.Resolve<IEventHandler<UserAssociatedWithIdentity>>().Handle);
            eventBus.RegisterEventHandler<UserCreated>(container.Resolve<IEventHandler<UserCreated>>().Handle);
        }
    }
}