namespace VideoHelp.ReadModel.Views
{
    public class AccountAssociationInputModel
    {
        public AccountAssociationInputModel(string accountIdentity)
        {
            AccountIdentity = accountIdentity;
        }

        public string AccountIdentity { get; set; }
    }
}