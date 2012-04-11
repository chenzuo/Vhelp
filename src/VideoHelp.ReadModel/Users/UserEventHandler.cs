using VideoHelp.Domain.Messages.Events.Users;
using VideoHelp.ReadModel.Contracts;
using VideoHelp.ReadModel.Meeting;
using VideoHelp.ReadModel.Notification;

namespace VideoHelp.ReadModel.Users
{
    public class UserEventHandler
    {
        private readonly IWriteRepository _writeRepository;
        private readonly INotificationBus _bus;

        public UserEventHandler(IWriteRepository writeRepository, INotificationBus bus)
        {
            _writeRepository = writeRepository;
            _bus = bus;
        }

        public void Handle(UserCreated @event)
        {
            var userView = new UserView(@event.AggregateId, @event.Nick, @event.FullName, @event.Email);
            _writeRepository.Add(userView);
            _writeRepository.SaveChanges();

            _bus.PublishNotification(new ViewUpdated<UserView>(userView.Id));
        }

        public void Handle(UserAssociatedWithIdentity @event)
        {
            var userAssociationView = new UserAssociationView(@event.AggregateId, @event.Identity);
            _writeRepository.Add(userAssociationView);
            _writeRepository.SaveChanges();

            _bus.PublishNotification(new ViewUpdated<UserAssociationView>(userAssociationView.UserId));
        }
    }
}