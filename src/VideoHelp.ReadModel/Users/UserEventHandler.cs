using VideoHelp.Domain.Messages.Events.Users;
using VideoHelp.ReadModel.Contracts;
using VideoHelp.ReadModel.Notification;

namespace VideoHelp.ReadModel.Users
{
    public class UserEventHandler
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly INotificationBus _bus;

        public UserEventHandler(IRepositoryFactory repositoryFactory, INotificationBus bus)
        {
            _repositoryFactory = repositoryFactory;
            _bus = bus;
        }

        public void Handle(UserCreated @event)
        {
            UserView userView;
            using (var repository = _repositoryFactory.Create())
            {
                userView = new UserView(@event.AggregateId, @event.Nick, @event.FullName, @event.Email);
                repository.Store(userView);
            }

            _bus.PublishNotification(new ViewUpdated<UserView>(userView.Id));
        }

        public void Handle(UserAssociatedWithIdentity @event)
        {
            UserAssociationView userAssociationView;
            using (var repository = _repositoryFactory.Create())
            {
                userAssociationView = new UserAssociationView(@event.AggregateId, @event.Identity);
                repository.Store(userAssociationView);
            }

            _bus.PublishNotification(new ViewUpdated<UserAssociationView>(userAssociationView.UserId));
        }
    }
}