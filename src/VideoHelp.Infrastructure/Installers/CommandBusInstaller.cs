using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MassTransit;
using VideoHelp.Infrastructure.MassTransit;

namespace VideoHelp.Infrastructure.Installers
{
    public class CommandBusInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var bus = new MassTransitCommandBus(container.Resolve<IServiceBus>());
            container.Register(Component.For<ICommandBus>().Instance(bus).LifeStyle.Singleton);
        }
    }
}