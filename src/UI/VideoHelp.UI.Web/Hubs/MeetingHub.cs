using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;
using SignalR;
using SignalR.Hosting;
using SignalR.Hubs;
using VideoHelp.Domain.Messages.Commands;
using VideoHelp.Infrastructure;
using VideoHelp.ReadModel;
using VideoHelp.ReadModel.Meeting;
using VideoHelp.ReadModel.Notification;
using System.Monads;
using System.Linq;

namespace VideoHelp.UI.Web.Hubs
{
    public class MeetingHub : Hub, IDisconnect, IConnected
    {
        private readonly ICommandBus _commandBus;
        private readonly IRepositoryFactory _repositoryFactory;


        public MeetingHub()
        {
            _commandBus = DependencyResolver.Current.GetService<ICommandBus>();
            _repositoryFactory = DependencyResolver.Current.GetService<IRepositoryFactory>();
        }

        public void JoinToMeeting(string meetingId, string userId)
        {
            GroupManager.AddToGroup(Context.ConnectionId, meetingId);
            var userGuid = new Guid(userId);
            var meetingGuid = new Guid(meetingId);

            using (var repository = _repositoryFactory.Create())
            {
                var streams = repository.GetById<MeetingCameraStreamsView>(meetingGuid);
                if (streams == null)
                {
                    return;
                }

                foreach (var stream in streams.CameraStreams.Where(content => content.OwnerUser != userGuid))
                {
                    Caller.updateCameraStream(stream.OwnerUser, stream.StreamLink);
                }

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