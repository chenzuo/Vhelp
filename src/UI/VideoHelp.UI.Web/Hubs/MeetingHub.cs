using System;
using System.Web.Mvc;
using SignalR.Hubs;
using VideoHelp.Domain.Messages.Commands;
using VideoHelp.Domain.Messages.ValueObject;
using VideoHelp.Infrastructure;
using VideoHelp.ReadModel.Contracts;
using VideoHelp.ReadModel.Notification;
using System.Monads;

namespace VideoHelp.UI.Web.Hubs
{
    public class MeetingHub : Hub
    {
        private readonly ICommandBus _commandBus;
        private readonly INotificationBus _notificationBus;
        private Guid _meetingId;
        private Action _unsubscribeNotificationAction;

        public MeetingHub()
        {
            _commandBus = DependencyResolver.Current.GetService<ICommandBus>();
            _notificationBus = DependencyResolver.Current.GetService<INotificationBus>();
        }

        public void InitializeMeeting(string meetingId, string userId)
        {
            var newMeeting = new Guid(meetingId);
            if(newMeeting != Guid.Empty)
            {
                _meetingId = newMeeting;
            }

            _unsubscribeNotificationAction.Do(action => action());
            _unsubscribeNotificationAction = _notificationBus.SubscribeNotification<MeetingViewUpdated>(meetingUpdated, _meetingId);
        }

        public void AddCameraStream(string meetingId, string userId, string streamLink)
        {
            _commandBus.Publish(new AttachMediaContent(new Guid(meetingId), new CameraStream(Guid.NewGuid(), new Guid(userId), streamLink)));
        }

        private void meetingUpdated(MeetingViewUpdated notification)
        {
            Clients.updateMeeting(notification.View);
        }

    }
}