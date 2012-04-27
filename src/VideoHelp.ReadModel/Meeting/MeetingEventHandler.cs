using VideoHelp.Domain.Messages.Events.MediaContent;
using VideoHelp.Domain.Messages.Events.Meeting;
using VideoHelp.ReadModel.Contracts;
using VideoHelp.ReadModel.Notification;
using System.Linq;

namespace VideoHelp.ReadModel.Meeting
{
    public class MeetingEventHandler
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly INotificationBus _notificationBus;

        public MeetingEventHandler(IRepositoryFactory repositoryFactory, INotificationBus notificationBus)
        {
            _repositoryFactory = repositoryFactory;
            _notificationBus = notificationBus;
        }

        public void Handle(MeetingCreated @event)
        {
            var meeting = new MeetingView(@event.AggregateId, @event.OwnerId, @event.Name, @event.CreationDate);
            using (var repository = _repositoryFactory.Create())
            {
                repository.Store(meeting);
            }

            _notificationBus.PublishNotification(new ViewUpdated<MeetingView>(meeting.Id));
        }

        public void Handle(CameraStreamCreated @event)
        {
            MeetingCameraStreamsView meetingStreams;
            using (var repository = _repositoryFactory.Create())
            {
                meetingStreams = repository.GetById<MeetingCameraStreamsView>(@event.MeetingId);
                if(meetingStreams == null)
                {
                    meetingStreams = new MeetingCameraStreamsView(@event.MeetingId);
                    repository.Store(meetingStreams);
                }

                var toRemoteContent = meetingStreams.CameraStreams.Where(content => content.OwnerUser == @event.OwnerUser).ToList();
                foreach (var remoteItem in toRemoteContent)
                {
                    meetingStreams.CameraStreams.Remove(remoteItem);
                }

                meetingStreams.CameraStreams.Add(new CameraStream(@event.OwnerUser, @event.StreamLink));
            }
            _notificationBus.PublishNotification(new ViewUpdated<MeetingCameraStreamsView>(meetingStreams.Id));
        } 
    }

   
}