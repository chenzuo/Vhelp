
namespace VideoHelp.UI.Domain.LoginzaAuthentication.ExtractStrategy
{
    public class GoogleStratagy : BaseStratagy
    {
        public GoogleStratagy() : base(@"https://www.google.com/accounts/o8/ud")
        {
        }

        public override AccountInformation GetProfile(dynamic value)
        {
            var firstName = value.name.first_name;
            var lastName = value.name.last_name;

            return new AccountInformation
                    {
                        Email = value.email,
                        FullName = value.name.full_name,
                        NickName = string.Join(" ", firstName, lastName),
                        Identity = value.identity,
                    };
        }
    }
}