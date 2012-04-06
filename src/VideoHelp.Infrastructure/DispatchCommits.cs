using EventStore;
using EventStore.Dispatcher;
using Magnum.Reflection;
using MassTransit;

namespace VideoHelp.Infrastructure
{
    public class DispatchCommits : IDispatchCommits
    {
        private readonly IServiceBus _serviceBus;

        public DispatchCommits(IServiceBus serviceBus)
        {
            _serviceBus = serviceBus;
        }

        void IDispatchCommits.Dispatch(Commit commit)
        {
            commit.Events.ForEach(@event => this.FastInvoke("publishEvent", @event.Body));
        }

        private void publishEvent<T>(T @event) where T : class
        {
            _serviceBus.Publish(@event);
        }

        public void Dispose(){}
    }
}