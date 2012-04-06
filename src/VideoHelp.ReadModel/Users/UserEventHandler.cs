using VideoHelp.Domain.Messages.Events.Users;
using VideoHelp.ReadModel.Contracts;
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
            _writeRepository.Add(new UserView(@event.AggregateId, @event.Nick, @event.FullName, @event.Email));
            _writeRepository.SaveChanges();
        }

        public void Handle(UserAssociatedWithIdentity @event)
        {
            _writeRepository.Add(new UserAssociationView(@event.AggregateId, @event.Identity));
            _writeRepository.SaveChanges();
            _bus.PublishNotification(new UserAssociationUpdated(@event.AggregateId));
        }
    }
}