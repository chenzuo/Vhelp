using System;
using VideoHelp.Domain.Messages;

namespace VideoHelp.Infrastructure
{
    public interface IEventBus
    {
        void RegisterEventHandler<T>(Action<T> eventAction) where T : DomainEvent;
    }
}