using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CommonDomain.Core;
using CommonDomain.Persistence;
using CommonDomain.Persistence.EventStore;
using EventStore;
using EventStore.Dispatcher;
using MassTransit;

namespace VideoHelp.Infrastructure.Installers
{
    public class EventStoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var eventStore = getInitializedEventStore(new DispatchCommits(container.Resolve<IServiceBus>()));
            container.Register(Component.For<IStoreEvents>().Instance(eventStore).LifeStyle.Singleton);

            var repository = new EventStoreRepository(eventStore, new AggregateFactory(), new ConflictDetector());
            container.Register(Component.For<IRepository>().Instance(repository).LifeStyle.Singleton);
        }

        private IStoreEvents getInitializedEventStore(IDispatchCommits dispatchCommits)
        {
            return Wireup.Init()
                .UsingRavenPersistence("Raven")
                .UsingAsynchronousDispatchScheduler(dispatchCommits)
                .Build();
        }
    }
}