using System.Linq;
using Raven.Client;
using VideoHelp.ReadModel.Indexes;

namespace VideoHelp.ReadModel.Views
{
    public class AccountAssociationViewFactory : IViewFactory<AccountAssociationInputModel, AccountAssociationView>
    {
        private readonly IDocumentStore _documentStore;

        public AccountAssociationViewFactory(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public AccountAssociationView Load(AccountAssociationInputModel input)
        {
            using (var session = _documentStore.OpenSession())
            {
                return session.Query<AccountAssociationView, AccountIdentityByUserId>()
                    .Customize(customization =>  customization.WaitForNonStaleResultsAsOfNow())
                    .FirstOrDefault(view => view.AccountIdentity == input.AccountIdentity);
            }
        }
    }
}