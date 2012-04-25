
using VideoHelp.UI.Utility;

namespace VideoHelp.UI.Domain.LoginzaAuthentication.ExtractStrategy
{
    public class YandexStratagy : BaseStratagy
    {
        public YandexStratagy()
            : base(@"http://openid.yandex.ru/server/")
        {
        }

        public override AccountInformation GetProfile(dynamic value)
        {
            return new AccountInformation
                       {
                           Email = value.email,
                           FirstName = value.name.full_name,
                           NickName = value.nickname,
                           Identity = value.identity,
                       };
        }
    }
}