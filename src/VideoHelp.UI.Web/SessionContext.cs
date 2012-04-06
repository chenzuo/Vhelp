using System.Web;
using Domain;

namespace VideoHelp
{
    public class SessionContext
    {
        public static User CurrentUser
        {
            get
            {
                return HttpContext.Current.Session["CurrentUser"] as User;
            }
            set
            {
                HttpContext.Current.Session["CurrentUser"] = value;
            }
        } 
    }
}