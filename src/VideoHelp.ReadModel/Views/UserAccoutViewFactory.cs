using System.Linq;
using Raven.Client;
using VideoHelp.ReadModel.Documents;

namespace VideoHelp.ReadModel.Views
{
    public class UserAccoutViewFactory : IViewFactory<UserAccoutViewInputModel, UserAccoutView>
    {
        private readonly IDocumentStore _documentStore;

        public UserAccoutViewFactory(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }


        public UserAccoutView Load(UserAccoutViewInputModel input)
        {
            using (var session = _documentStore.OpenSession())
            {
                return session.Query<UserDocument>()
                    .Where(document => document.Id == input.UserId)
                    .Select(document => new UserAccoutView
                                            {
                                                Email = document.Email,
                                                UserId = document.Id,
                                                Nick = document.Nick,
                                            })
                    .FirstOrDefault();
            }
        }
    }
}