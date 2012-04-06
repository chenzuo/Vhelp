using System.Web.Mvc;
namespace VideoHelp.UI.Web.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult OnlineUsers()
        {
            if (UserManager.IsUserLogged)
            {
                //ServiceClient.Current.SendOneWay(new UserActivityRequest(UserManager.CurrentUser.Id));
            }
            
            //var response = ServiceClient.Current.Send<OnlineUsersResponse>(new OnlineUsersRequest());
            return null;// Json(response.OnlineUsers, JsonRequestBehavior.AllowGet);
        }
    }
}