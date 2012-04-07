using System.Web;
using VideoHelp.ReadModel.Users;

namespace VideoHelp.UI.Web
{
    public static class UserManager
    {
        private const string CURRENT_USER = "CurrentUser";

        public static bool IsUserLogged
        {
            get { return CurrentUser != null; }
        }

        public static UserView CurrentUser
        {
            get
            {
                return HttpContext.Current == null ? null : HttpContext.Current.Session[CURRENT_USER] as UserView;
            }
        }

        public static void Loggin(UserView user)
        {
            HttpContext.Current.Session[CURRENT_USER] = user;
        }

        public static void Logout()
        {
            if(!IsUserLogged)
            {
                return;
            }

            HttpContext.Current.Session[CURRENT_USER] = null;
        }
    }
}