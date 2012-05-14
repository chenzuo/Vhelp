using Raven.Client.Document;
using VideoHelp.ReadModel.Documents;

namespace VideoHelp.ReadModel.Views
{
    public class MeetingViewFactory : IViewFactory<MeetingInputModel, MeetingView>
    {
        private readonly DocumentStore _documentStore;

        public MeetingViewFactory(DocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public MeetingView Load(MeetingInputModel input)
        {
            using (var session = _documentStore.OpenSession())
            {
                var doc = session.Load<MeetingDocument>(input.MeetingId);

                return new MeetingView(doc.Id, doc.Name, doc.WebCameraStreams);
            }
        }
    }
}