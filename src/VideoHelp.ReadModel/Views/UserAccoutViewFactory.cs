using Raven.Client;
using VideoHelp.ReadModel.Documents;

namespace VideoHelp.ReadModel.Views
{
    public class UserAccoutViewFactory : IViewFactory<UserAccoutInputModel, UserAccoutView>
    {
        private readonly IDocumentStore _documentStore;

        public UserAccoutViewFactory(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }


        public UserAccoutView Load(UserAccoutInputModel input)
        {
            using (var session = _documentStore.OpenSession())
            {
                var document = session.Load<UserDocument>(RavenDb.GetId<UserDocument>(input.UserId));
                return new UserAccoutView
                {
                    Email = document.Email,
                    UserId = document.DocumentId,
                    Nick = document.Nick,
                };
            }
        }
    }
}