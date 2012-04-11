using VideoHelp.Domain.Messages.Events.Meeting;
using VideoHelp.ReadModel.Contracts;
using VideoHelp.ReadModel.Notification;
using VideoHelp.ReadModel.Users;

namespace VideoHelp.ReadModel.Meeting
{
    public class MeetingListEventHandler
    {
        private readonly IWriteRepository _writeRepository;
        private readonly IReadRepository _readRepository;
        private readonly INotificationBus _notificationBus;

        public MeetingListEventHandler(IWriteRepository writeRepository, IReadRepository readRepository, INotificationBus notificationBus)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _notificationBus = notificationBus;
        }

        public void Handle(MeetingCreated @event)
        {
            var meetingListView = new MeetingListView(@event.AggregateId, @event.OwnerId, _readRepository.GetById<UserView>(@event.OwnerId).Nick, @event.Name, @event.CreationDate);
            _writeRepository.Add(meetingListView);
            _writeRepository.SaveChanges();
            _notificationBus.PublishNotification(new ViewUpdated<MeetingListView>(meetingListView.Id));
        }
    }
}