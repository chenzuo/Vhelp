using VideoHelp.Domain.Messages;

namespace VideoHelp.Infrastructure.MassTransit
{
    public class QueryCommand<T> where T : DomainCommand
    {
        public QueryCommand(T command)
        {
            Command = command;
        }

        public T Command { get; private set; }
    }
}