using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MassTransit;
using VideoHelp.Infrastructure.MassTransit;

namespace VideoHelp.Infrastructure.Installers
{
    public class EventBusInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var bus = new MassTransitEventBus(container.Resolve<IServiceBus>());
            container.Register(Component.For<IEventBus>().Instance(bus).LifeStyle.Singleton);
        }
    }
}