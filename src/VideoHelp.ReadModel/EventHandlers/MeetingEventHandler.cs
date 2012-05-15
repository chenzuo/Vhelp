using System.Linq;
using Raven.Client;
using VideoHelp.Domain.Messages.Events.MediaContent;
using VideoHelp.Domain.Messages.Events.Meeting;
using VideoHelp.ReadModel.Documents;

namespace VideoHelp.ReadModel.EventHandlers
{
    public class MeetingEventHandler : IEventHandler<MeetingCreated>, IEventHandler<CameraStreamCreated>
    {
        private readonly IDocumentStore _documentStore;
        private readonly INotificationBus _bus;

        public MeetingEventHandler(IDocumentStore documentStore, INotificationBus bus)
        {
            _documentStore = documentStore;
            _bus = bus;
        }

        public void Handle(MeetingCreated @event)
        {
            using (var session = _documentStore.OpenSession())
            {
                var doc = new MeetingDocument
                              {
                                  DocumentId = @event.AggregateId,
                                  Name = @event.Name,
                                  Owner = @event.OwnerId,
                                  CreationDate = @event.CreationDate
                              };

                session.Store(doc);
                session.SaveChanges();

                _bus.PublishNotification(doc);
            }
        }

        public void Handle(CameraStreamCreated @event)
        {
            using (var session = _documentStore.OpenSession())
            {
                var doc = session.Load<MeetingDocument>(RavenDb.GetId<MeetingDocument>(@event.MeetingId));

                var stream = doc.WebCameraStreams.FirstOrDefault(cameraStream => cameraStream.OwnerUser == @event.OwnerUser);
                if(stream == null)
                {
                    doc.WebCameraStreams.Add(new WebCameraStream(@event.OwnerUser, @event.StreamSource));
                }
                else
                {
                    stream.StreamSource = @event.StreamSource;
                }

                session.SaveChanges();
                _bus.PublishNotification(doc);
            }
        }
    }
}