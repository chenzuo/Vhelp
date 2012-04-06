using System;
using System.IO;
using System.Net;
using System.Monads;
using System.Web.Helpers;

namespace VideoHelp.UI.Domain.LoginzaAuthentication
{
    public class AccountInformationExtractor
    {
        private readonly IExtractProfileInformationStrategy[] _extractStrategies;
        private readonly Uri _serviceUri;

        private readonly int _widgetId;
        private readonly string _secureKey;
        private readonly bool _checkSecure;

        public AccountInformationExtractor(params IExtractProfileInformationStrategy[] extractStrategies)
        {
            _extractStrategies = extractStrategies.CheckNull("extractStrategies");
            _serviceUri = new Uri("http://loginza.ru/api/authinfo");
            _checkSecure = false;
        }

        public AccountInformationExtractor(int widgetId, string secureKey, params IExtractProfileInformationStrategy[] extractStrategies) : this(extractStrategies)
        {
            _widgetId = widgetId;
            _secureKey = secureKey.CheckNull("secureKey");
            _checkSecure = true;
        }

        public AccountInformation Extract(String token)
        {
            var response = processRequest(composeRequestUrl(token));
            var result = Json.Decode(response);

            foreach (var strategy in _extractStrategies)
            {
                if (strategy.IsSupported(result.Provider))
                {
                    return strategy.GetProfile(result);
                }
            }

            return null;
        }


        private string processRequest(string requestUrl)
        {
            var request = WebRequest.Create(requestUrl);
            request.Method = "GET";

            using (var response = request.GetResponse())
            {
                var responseStream = response.GetResponseStream();

                if (responseStream == null)
                    throw new WebException("Response stream is empty"); 

                var dataStream = new StreamReader(responseStream);
                return dataStream.ReadToEnd();
            }
        }

        private string composeRequestUrl(String token)
        {
            string requestParams = string.Format("?token={0}", token);

            if (_checkSecure)
            {
                var sign = (token + _secureKey).GetMd5Hash();
                requestParams += string.Format("&id={0}&sig={1}", _widgetId, sign);
            }

            return _serviceUri + requestParams;
        }
    }
}