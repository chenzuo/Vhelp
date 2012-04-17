using System;
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
            var meeting =_readRepository.GetById<MeetingView>(@event.MeetingId);

            var toRemoteContent = meeting.MediaContents.OfType<CameraStream>().Where(content => content.OwnerUser == @event.OwnerUser);
            foreach (var remoteItem in toRemoteContent)
            {
                meeting.MediaContents.Remove(remoteItem);
            }

            meeting.MediaContents.Add( new CameraStream(@event.AggregateId, @event.OwnerUser, @event.StreamLink));
            _readRepository.SaveChanges();
            _notificationBus.PublishNotification(new ViewUpdated<MeetingView>(meeting.Id));
        } 
    }

    public class CameraStream : MediaContent
    {
        public CameraStream(Guid id, Guid ownerUser, string streamLink) : base(id, ownerUser)
        {
            StreamLink = streamLink;
        }

        public string StreamLink { get; private set; }
    }
}