using System;
using Magnum.Threading;
using MassTransit;
using VideoHelp.Domain.CommandHandlers;
using VideoHelp.Domain.Messages;
using VideoHelp.Infrastructure.MassTransit.Consumers;

namespace VideoHelp.Infrastructure.MassTransit
{
    public class MassTransitCommandBus : ICommandBus
    {
        private readonly IServiceBus _serviceBus;

        public MassTransitCommandBus(IServiceBus serviceBus)
        {
            _serviceBus = serviceBus;
        }

        public void Publish<T>(T command) where T : DomainCommand
        {
            _serviceBus.Publish(command);
            
        }

        public CommandResult Send<T>(T command, int timeoutInSec = 30) where T : DomainCommand
        {
            CommandResult result = null;
            _serviceBus.PublishRequest(new QueryCommand<T>(command), context =>
                                                                         {
                                                                             context.Handle<CommandResult>(commandResult => result = commandResult);
                                                                             context.SetTimeout(new TimeSpan(0, 0, timeoutInSec));
                                                                         });

            return result ?? CommandResult.TimeOut(timeoutInSec);
        }

        public IAsyncResult BeginSend<T>(T command, Action<CommandResult> action, int timeoutInSec = 30) where T : DomainCommand
        {
            var callback = new AsyncCallback(result => action((CommandResult)result.AsyncState));

            return _serviceBus.BeginPublishRequest(new QueryCommand<T>(command), callback, null, context =>
                                                                                                     {

                                                                                                         context.Handle<CommandResult>(commandResult => new AsyncResult(callback, commandResult));
                                                                                                         context.SetTimeout(new TimeSpan(0, 0, timeoutInSec));
                                                                                                     });
        }

        public bool EndSend(IAsyncResult result)
        {
            return _serviceBus.EndRequest(result);
        }
        
        public void RegisterCommandHandler<T>(ICommandHandler<T> handler) where T : DomainCommand
        {
            _serviceBus.SubscribeConsumer(() => new DomainCommandConsumer<T>(handler));
            _serviceBus.SubscribeConsumer(() => new DomainCommandWithResultConsumer<T>(handler));
        }
    }
}