using System;
using MassTransit;
using VideoHelp.Domain.CommandHandlers;
using VideoHelp.Domain.Messages;

namespace VideoHelp.Infrastructure.MassTransit.Consumers
{
    public class DomainCommandWithResultConsumer<T> : Consumes<QueryCommand<T>>.All where T : DomainCommand
    {
        private readonly ICommandHandler<T> _handler;

        public DomainCommandWithResultConsumer(ICommandHandler<T> handler)
        {
            _handler = handler;
        }
        
        public void Consume(QueryCommand<T> message)
        {
            try
            {
                _handler.Handle(message.Command);
                this.MessageContext().Respond(CommandResult.Success());
            }
            catch (Exception ex)
            {
                this.MessageContext().Respond(CommandResult.Exception(ex));
            }
        }
    }
}