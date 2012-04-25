using System.IO;
using System.Net;
using System.Web.Helpers;
namespace VideoHelp.UI.Utility.UloginAuthentication
{
    public class UloginAccountInformationExtractor
    {
        private readonly string _link;

        public UloginAccountInformationExtractor(string token, string host)
        {
            _link = string.Format("http://ulogin.ru/token.php?token={0}&host={1}", token, host);
        }

        public dynamic Extract()
        {
            return Json.Decode(processRequest(_link));
        }

        private static string processRequest(string requestUrl)
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
    }
}