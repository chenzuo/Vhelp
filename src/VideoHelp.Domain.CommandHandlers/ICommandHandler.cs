using VideoHelp.Domain.Messages;

namespace VideoHelp.Domain.CommandHandlers
{
    public interface ICommandHandler<in T>  where T : DomainCommand
    {
        void Handle(T command);
    }
}