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
using VideoHelp.ReadModel.Contracts;
using VideoHelp.ReadModel.Meeting;
using VideoHelp.ReadModel.Notification;
using System.Monads;
using System.Linq;

namespace VideoHelp.UI.Web.Hubs
{
    public class MeetingHub : Hub, IDisconnect, IConnected
    {
        private readonly ICommandBus _commandBus;
        private readonly INotificationBus _notificationBus;
        private readonly IReadRepository _readRepository;
        private Action _cancelNotification;


        public MeetingHub()
        {
            _commandBus = DependencyResolver.Current.GetService<ICommandBus>();
            _notificationBus = DependencyResolver.Current.GetService<INotificationBus>();
            _readRepository = DependencyResolver.Current.GetService<IReadRepository>();

            
        }

        public void JoinToMeeting(string meetingId, string userId)
        {
            GroupManager.AddToGroup(Context.ConnectionId, meetingId);
            var userGuid = new Guid(userId);
            var meetingGuid = new Guid(meetingId);
            var cameraStreams = _readRepository.GetById<MeetingView>(meetingGuid).MediaContents.Where(content => content.OwnerUser != userGuid).OfType<CameraStream>();
            foreach (var stream in cameraStreams)
            {
                Clients[meetingId].updateCameraStream(stream.OwnerUser, stream.StreamLink);
            }
        }

        private void meetingUpdated(Guid guid)
        {
            //Clients.updateMeeting(notification.View);
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