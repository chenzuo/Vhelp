using System;
using VideoHelp.Domain.CommandHandlers;
using VideoHelp.Domain.Messages;

namespace VideoHelp.Infrastructure
{
    public interface ICommandBus
    {
        void Publish<T>(T command) where T : DomainCommand;
        CommandResult Send<T>(T command, int timeoutInSec = 30) where T : DomainCommand;

        IAsyncResult BeginSend<T>(T command, Action<CommandResult> action, int timeoutInSec = 30) where T : DomainCommand;
        bool EndSend(IAsyncResult result);

        void RegisterCommandHandler<T>(ICommandHandler<T> handler) where T : DomainCommand;
    }
}