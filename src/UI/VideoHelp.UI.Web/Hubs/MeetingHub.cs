using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;
using SignalR;
using SignalR.Hosting;
using SignalR.Hubs;
using VideoHelp.Domain.Messages.Commands;
using VideoHelp.Infrastructure;
using VideoHelp.ReadModel;
using VideoHelp.ReadModel.Views;

namespace VideoHelp.UI.Web.Hubs
{
    public class MeetingHub : Hub, IDisconnect, IConnected
    {
        private readonly ICommandBus _commandBus;
        private readonly IViewRepository _viewRepository;


        public MeetingHub()
        {
            _commandBus = DependencyResolver.Current.GetService<ICommandBus>();
            _viewRepository = DependencyResolver.Current.GetService<IViewRepository>();
        }

        public void JoinToMeeting(string meetingId, string userId)
        {
            GroupManager.AddToGroup(Context.ConnectionId, meetingId);
            var userGuid = new Guid(userId);

            var streams = _viewRepository.Load<MeetingInputModel, MeetingView>(new MeetingInputModel(new Guid(meetingId)));


            if (streams == null)
            {
                return;
            }

            foreach (var stream in streams.WebCameraStreams.Where(content => content.OwnerUser != userGuid))
            {
                Caller.updateCameraStream(stream.OwnerUser, stream.StreamSource);
            }

        }

        public void AttachCameraStream(string meetingId, string userId, string farId)
        {
            Clients[meetingId].updateCameraStream(userId, farId);
            _commandBus.Publish(new AttachCameraStream(new Guid(meetingId), new Guid(userId), farId));
        }


        public Task Disconnect()
        {
            Debug.WriteLine("DiСоnnected " + Context.ConnectionId);
            return null;
        }


        public Task Connect()
        {
            Debug.WriteLine("Соnnected " + Context.ConnectionId);
            return null;
        }

        public Task Reconnect(IEnumerable<string> groups)
        {
            //throw new NotImplementedException();
            return null;
        }
    }

    public class UserConnectionIdFactory : IConnectionIdFactory
    {
        public string CreateConnectionId(IRequest request, IPrincipal user)
        {
            return UserManager.CurrentUser.ToString();
        }
    }
}