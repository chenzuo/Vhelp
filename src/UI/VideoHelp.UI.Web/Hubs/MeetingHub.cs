using System;
using System.Web.Mvc;
using SignalR.Hubs;
using VideoHelp.Infrastructure;

namespace VideoHelp.UI.Web.Hubs
{
    public class MeetingHub : Hub
    {
        private readonly ICommandBus _commandBus;

        public MeetingHub()
        {
            _commandBus = DependencyResolver.Current.GetService<ICommandBus>();
        }

        public void CreateVideoStream(string userId, string streamId)
        {
            //Clients.AddChatMessage(message);
        }

        public void AddVideoStream()
        {
            Clients.addVideoStream();
        }
    }
}