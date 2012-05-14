using VideoHelp.Domain.Messages.Events.Meeting;
using VideoHelp.ReadModel.Notification;
using VideoHelp.ReadModel.Users;

namespace VideoHelp.ReadModel.Meeting
{
    public class MeetingListEventHandler
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly INotificationBus _notificationBus;

        public MeetingListEventHandler(IRepositoryFactory repositoryFactory, INotificationBus notificationBus)
        {
            _repositoryFactory = repositoryFactory;
            _notificationBus = notificationBus;
        }

        public void Handle(MeetingCreated @event)
        {
            MeetingListView meetingListView;
            using (var repository = _repositoryFactory.Create())
            {
                meetingListView = new MeetingListView(@event.AggregateId, @event.OwnerId, repository.GetById<UserView>(@event.OwnerId).Nick, @event.Name, @event.CreationDate);
                repository.Store(meetingListView);
            }

            _notificationBus.PublishNotification(new ViewUpdated<MeetingListView>(meetingListView.Id));
        }
    }
}