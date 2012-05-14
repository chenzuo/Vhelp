using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MassTransit;

namespace VideoHelp.ReadModel.Infrastructure.Installers
{
    public class NotificationBusInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var bus = new NotificationBus(container.Resolve<IServiceBus>());
            container.Register(Component.For<INotificationBus>().Instance(bus).LifeStyle.Singleton);
        }
    }
}