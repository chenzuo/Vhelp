using System;
using System.Web;
using System.Monads;

namespace VideoHelp.UI.Web
{
    public static class UserManager
    {
        private const string CURRENT_USER = "CurrentUser";
        public const string CURRENT_USER_NAME = "CurrentUserName";

        public static bool IsUserLogged
        {
            get { return CurrentUser != Guid.Empty; }
        }

        public static Guid CurrentUser
        {
            get
            {
                if(HttpContext.Current != null )
                {
                    HttpContext.Current.Request.Cookies.Get(CURRENT_USER_NAME).Do(cookie => cookie.Expires = DateTime.Now.AddHours(1));

                    var  cookies = HttpContext.Current.Request.Cookies.Get(CURRENT_USER);
                    if(cookies != null)
                    {
                        cookies.Expires = DateTime.Now.AddHours(1);
                        return new Guid(cookies.Value);
                    }
                }

                return  Guid.Empty;
            }
        }

        public static void Loggin(Guid userId, string userName)
        {
            var userIdCookie = new HttpCookie(CURRENT_USER, userId.ToString()) {Expires = DateTime.Now.AddHours(1)};
            var userNameCookie = new HttpCookie(CURRENT_USER_NAME, userName) { Expires = DateTime.Now.AddHours(1) };

            HttpContext.Current.Response.Cookies.Add(userIdCookie);
            HttpContext.Current.Response.Cookies.Add(userNameCookie);
        }

        public static void Logout()
        {
            if(!IsUserLogged || HttpContext.Current == null)
            {
                return;
            }

            var currentUserCookie = HttpContext.Current.Request.Cookies.Get(CURRENT_USER).Do(cookie => cookie.Expires = cookie.Expires.AddYears(-1));
            var currentUserNameCookie = HttpContext.Current.Request.Cookies.Get(CURRENT_USER_NAME).Do(cookie => cookie.Expires = cookie.Expires.AddYears(-1));

            HttpContext.Current.Response.Cookies.Add(currentUserCookie);
            HttpContext.Current.Response.Cookies.Add(currentUserNameCookie);
        }
    }
}