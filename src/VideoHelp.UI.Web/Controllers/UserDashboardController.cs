using System.Web.Mvc;

namespace VideoHelp.UI.Web.Controllers
{
    public class UserDashboardController : Controller
    {
        //
        // GET: /UserDashboard/

        public ActionResult Index()
        {
            return View(UserManager.CurrentUser);
        }

    }
}
