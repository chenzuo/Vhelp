using System;
using SignalR.Hubs;
using VideoHelp.Infrastructure;

namespace VideoHelp.UI.Web.Hubs
{
    public class MeetingHub : Hub
    {
        private readonly ICommandBus _commandBus;
        private string a;
        public MeetingHub(ICommandBus commandBus)
        {
            _commandBus = commandBus;
            a = "fd";
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