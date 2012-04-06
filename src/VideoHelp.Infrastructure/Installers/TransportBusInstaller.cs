using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MassTransit;
using ServiceBusFactory = VideoHelp.Infrastructure.Factories.ServiceBusFactory;

namespace VideoHelp.Infrastructure.Installers
{
    public class TransportBusInstaller : IWindsorInstaller
    {
        private readonly Uri _endpoint;

        public TransportBusInstaller(Uri endpoint)
        {
            _endpoint = endpoint;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var massTransitBus = new ServiceBusFactory().Create(_endpoint);

            container.Register(Component.For<IServiceBus>().Instance(massTransitBus));
        }
    }
}