

namespace VideoHelp.UI.Domain.LoginzaAuthentication.ExtractStrategy
{
    public class VkStratagy : BaseStratagy
    {
        public VkStratagy() : base(@"http://vkontakte.ru/")
        {
        }

        public override AccountInformation GetProfile(dynamic value)
        {
            var firstName = value.name.first_name;
            var lastName = value.name.last_name;

            return new AccountInformation
                       {
                           Email = value.email,
                           FullName = string.Join(" ", firstName, lastName),
                           NickName = string.Join(" ", firstName, lastName),
                           Identity = value.identity,
                       };
        }
    }
}