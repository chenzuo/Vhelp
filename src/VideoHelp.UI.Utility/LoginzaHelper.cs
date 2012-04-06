using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;

namespace VideoHelp.UI.Utility
{
    /// <summary>
    /// Класс обеспечивает работу с аутентификацией Loginza
    /// </summary>
    public class LoginzaHelper
    {
        /// <summary>
        /// Адрес сервиса проверки токена
        /// </summary>
        /// <remarks>По умолчанию используется адрес http://loginza.ru/api/authinfo </remarks>
        public Uri ServiceUri { get; set; }

        /// <summary>
        /// Токен аутентификации
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// ID виджета
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Секретный ключ
        /// </summary>
        public string SecureKey { get; set; }

        /// <summary>
        /// Безопасный режим проверки token
        /// </summary>
        public bool IsSecureCheck { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public LoginzaHelper()
        {
            ServiceUri = new Uri(@"http://loginza.ru/api/authinfo"); //По умолчанию текущий сервис
            IsSecureCheck = false;
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="id">ID виджета</param>
        public LoginzaHelper(int id)
            : this()
        {
            Id = id;
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="id">ID виджета</param>
        /// <param name="secureKey">Секретный ключ</param>
        public LoginzaHelper(int id, string secureKey)
            : this(id)
        {
            SecureKey = secureKey;
        }

        /// <summary>
        /// Информация об аутентификации
        /// </summary>
        /// <param name="token">Уникальный идентификатор ячейки хранения результата авторизации</param>
        /// <returns>Информаци об аутентифицированном пользователе, либо сообщение об ошибке</returns>
        public dynamic AuthInfo(string token)
        {
            Token = token;
            var requestParams = string.Format("?token={0}", Token);
            if (IsSecureCheck)
            {
                var sign = ComputeMd5Hash(Token + SecureKey);
                requestParams += string.Format("&id={0}&sig={1}", Id, sign);
            }
            var response = GetResponse(requestParams, ServiceUri);
            var authObject = GetAuthObject(response);
            return authObject;
        }

        private static dynamic GetAuthObject(string response)
        {
            return Json.Decode(response);
        }

        private static string GetResponse(string requestParams, Uri serviceUri)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(serviceUri + requestParams);
                request.Method = "GET";

                using (var response = request.GetResponse())
                {
                    var responseStream = response.GetResponseStream();

                    if (responseStream == null)
                        throw new WebException("Response stream is empty"); //Хотя 

                    var dataStream = new StreamReader(responseStream);

                    return dataStream.ReadToEnd();

                }
            }
            catch (WebException exc)
            {
                throw new WebException("Could not get a response from the server.", exc);
            }

        }

        private static string ComputeMd5Hash(string inputString)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var buf = Encoding.Default.GetBytes(inputString);
                var hash = md5.ComputeHash(buf);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}