
using VideoHelp.UI.Utility;

namespace VideoHelp.UI.Domain.LoginzaAuthentication.ExtractStrategy
{
    public class MailRuStratagy : BaseStratagy
    {
        public MailRuStratagy() : base(@"http://mail.ru/")
        {
        }

        public override AccountInformation GetProfile(dynamic value)
        {
            var firstName = value.name.first_name;
            var lastName = value.name.last_name;

            return new AccountInformation
                    {
                        Email = value.email,
                        FirstName = firstName,
                        NickName = value.nickname,
                        Identity = value.identity,
                    };
        }
    }
}