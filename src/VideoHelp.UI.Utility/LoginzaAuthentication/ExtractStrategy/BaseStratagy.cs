using System;
using System.Monads;

namespace VideoHelp.UI.Domain.LoginzaAuthentication.ExtractStrategy
{
    public abstract class BaseStratagy : IExtractProfileInformationStrategy
    {
        private readonly string _providerName;

        protected BaseStratagy(string providerName)
        {
            _providerName = providerName.CheckNull(providerName);
        }

        public abstract AccountInformation GetProfile(dynamic value);

        public bool IsSupported(string provider)
        {
            return string.Equals(_providerName, provider, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}