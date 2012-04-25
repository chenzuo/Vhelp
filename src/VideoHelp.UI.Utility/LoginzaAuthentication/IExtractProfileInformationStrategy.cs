using VideoHelp.UI.Utility;

namespace VideoHelp.UI.Domain.LoginzaAuthentication
{
    public interface IExtractProfileInformationStrategy
    {
        AccountInformation GetProfile(dynamic value);
        bool IsSupported(string provider);
    }
}