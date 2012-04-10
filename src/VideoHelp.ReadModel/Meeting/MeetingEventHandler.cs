using VideoHelp.Domain.Messages.Events.Meeting;
using VideoHelp.ReadModel.Contracts;
using VideoHelp.ReadModel.Notification;

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
            _writeRepository.Add(new MeetingView(@event.AggregateId, @event.OwnerId, @event.Name, @event.CreationDate));
            _writeRepository.SaveChanges();
        }

        public void Handle(MediaContentAdded @event)
        {
            var meeting =_readRepository.GetById<MeetingView>(@event.AggregateId);
            meeting.MediaContents.Add(@event.Content);
            _readRepository.SaveChanges();
            _notificationBus.PublishNotification(new MeetingViewUpdated(meeting));
        } 
    }
}