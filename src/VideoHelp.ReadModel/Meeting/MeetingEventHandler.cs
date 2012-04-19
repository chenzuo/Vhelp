using VideoHelp.Domain.Messages.Events.MediaContent;
using VideoHelp.Domain.Messages.Events.Meeting;
using VideoHelp.ReadModel.Contracts;
using VideoHelp.ReadModel.Notification;
using System.Linq;

namespace VideoHelp.ReadModel.Meeting
{
    public class MeetingEventHandler
    {
        private readonly IWriteRepository _writeRepository;
        private readonly IReadRepository _readRepository;
        private readonly INotificationBus _notificationBus;

        public MeetingEventHandler(IWriteRepository writeRepository, IReadRepository readRepository, INotificationBus notificationBus)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _notificationBus = notificationBus;
        }

        public void Handle(MeetingCreated @event)
        {
            var meeting = new MeetingView(@event.AggregateId, @event.OwnerId, @event.Name, @event.CreationDate);
            _writeRepository.Add(meeting);
            _writeRepository.SaveChanges();
            _notificationBus.PublishNotification(new ViewUpdated<MeetingView>(meeting.Id));
        }

        public void Handle(CameraStreamCreated @event)
        {
            var meetingStreams =_readRepository.GetById<MeetingCameraStreamsView>(@event.MeetingId);
            
            if(meetingStreams == null)
            {
                meetingStreams = new MeetingCameraStreamsView(@event.MeetingId);
                meetingStreams.CameraStreams.Add(new CameraStream(@event.OwnerUser, @event.StreamLink));
                _writeRepository.Add(meetingStreams);
                _writeRepository.SaveChanges();
            }
            else
            {
                var toRemoteContent = meetingStreams.CameraStreams.Where(content => content.OwnerUser == @event.OwnerUser);
                foreach (var remoteItem in toRemoteContent)
                {
                    meetingStreams.CameraStreams.Remove(remoteItem);
                }

                meetingStreams.CameraStreams.Add(new CameraStream(@event.OwnerUser, @event.StreamLink));
                _readRepository.SaveChanges();
            }
            
            _notificationBus.PublishNotification(new ViewUpdated<MeetingCameraStreamsView>(meetingStreams.Id));
        } 
    }

   
}