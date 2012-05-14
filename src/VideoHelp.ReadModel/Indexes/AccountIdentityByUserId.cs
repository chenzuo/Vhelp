using System.Linq;
using Raven.Client.Indexes;
using VideoHelp.ReadModel.Documents;
using VideoHelp.ReadModel.Views;

namespace VideoHelp.ReadModel.Indexes
{
    public class AccountIdentityByUserId : AbstractIndexCreationTask<UserDocument, AccountAssociationView>
    {
        public AccountIdentityByUserId()
        {
            Map = users => from user in users
                           from association in user.AccountAssociations
                           select new
                            {
                                AccountIdentity = association.Identity,
                                UserId = user.Id
                            };


            Reduce = results => from result in results select result;

        }
    }
}