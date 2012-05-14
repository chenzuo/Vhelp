using System.Linq;
using Raven.Client;
using VideoHelp.ReadModel.Documents;

namespace VideoHelp.ReadModel.Views
{
    public class MeetingBrowseViewFactory : IViewFactory<MeetingBrowseInputModel, MeetingBrowseView>
    {
        private readonly IDocumentStore _documentStore;

        public MeetingBrowseViewFactory(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public MeetingBrowseView Load(MeetingBrowseInputModel input)
        {
            using(var session = _documentStore.OpenSession())
            {
                input.PageSize = (input.PageSize == 0 || input.PageSize > 20) ? 20 : input.PageSize;

                var query = session.Query<MeetingDocument>()
                    .Skip(input.Page*input.PageSize)
                    .Take(input.PageSize);

                if (!string.IsNullOrEmpty(input.SearchText))
                {
                    query = query.Where(x => x.Name.StartsWith(input.SearchText));
                }
                var b = query.Select(document => "sdsa").ToList();

                var items = query
                    .Select(x => new MeetingBrowseItem(x.Id, "", x.Name, x.CreationDate))
                    .ToList();

                return new MeetingBrowseView(input.PageSize, input.Page, input.SearchText, items);
            }

        }
    }
}