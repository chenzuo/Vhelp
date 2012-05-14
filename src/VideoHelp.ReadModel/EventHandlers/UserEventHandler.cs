using Raven.Client;
using VideoHelp.Domain.Messages.Events.Users;
using VideoHelp.ReadModel.Documents;

namespace VideoHelp.ReadModel.EventHandlers
{
    public class UserEventHandler : IEventHandler<UserCreated>, IEventHandler<UserAssociatedWithIdentity>
    {
        private readonly IDocumentStore _documentStore;
        private readonly INotificationBus _bus;

        public UserEventHandler(IDocumentStore documentStore, INotificationBus bus)
        {
            _documentStore = documentStore;
            _bus = bus;
        }

        public void Handle(UserCreated @event)
        {
            using (var session = _documentStore.OpenSession())
            {
                var doc = new UserDocument(@event.AggregateId, @event.Nick, @event.FirstName, @event.LastName, @event.Email);
                session.Store(doc);
                session.SaveChanges();
                _bus.PublishNotification(doc); 
            }
        }

        public void Handle(UserAssociatedWithIdentity @event)
        {
            using (var session = _documentStore.OpenSession())
            {
                var doc = session.Load<UserDocument>(@event.AggregateId);
                doc.AccountAssociations.Add(new AccountAssociationDocument{Identity = @event.Identity, Network = @event.Network});
                session.SaveChanges();

                _bus.PublishNotification(doc);
            }
        }
    }
}