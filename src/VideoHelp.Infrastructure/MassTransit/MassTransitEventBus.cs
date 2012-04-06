using System;
using MassTransit;
using VideoHelp.Domain.Messages;

namespace VideoHelp.Infrastructure.MassTransit
{
    public class MassTransitEventBus : IEventBus
    {
        private readonly IServiceBus _serviceBus;

        public MassTransitEventBus(IServiceBus serviceBus)
        {
            _serviceBus = serviceBus;
        }

        public void RegisterEventHandler<T>(Action<T> eventAction) where T : DomainEvent
        {
            _serviceBus.SubscribeHandler(eventAction); 
        }
    }
}