using MassTransit;
using VideoHelp.Domain.CommandHandlers;
using VideoHelp.Domain.Messages;

namespace VideoHelp.Infrastructure.MassTransit.Consumers
{
    public class DomainCommandConsumer<T> : Consumes<T>.All where T : DomainCommand
    {
        private readonly ICommandHandler<T> _handler;

        public DomainCommandConsumer(ICommandHandler<T> handler)
        {
            _handler = handler;
        }

        public void Consume(T command)
        {
            _handler.Handle(command);
        }
    }
}