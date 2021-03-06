using Raven.Client;
using VideoHelp.ReadModel.Documents;

namespace VideoHelp.ReadModel.Views
{
    public class MeetingViewFactory : IViewFactory<MeetingInputModel, MeetingView>
    {
        private readonly IDocumentStore _documentStore;

        public MeetingViewFactory(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public MeetingView Load(MeetingInputModel input)
        {
            using (var session = _documentStore.OpenSession())
            {
                var doc = session.Load<MeetingDocument>(RavenDb.GetId<MeetingDocument>(input.MeetingId));

                return new MeetingView(doc.DocumentId, doc.Name, doc.WebCameraStreams);
            }
        }
    }
}