using VideoHelp.Domain.Messages;

namespace VideoHelp.ReadModel
{
    public interface IEventHandler{}

    public interface IEventHandler<in T> : IEventHandler where T : DomainEvent
    {
        void Handle(T @event);
    }
}