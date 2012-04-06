using System.Web.Mvc;

namespace VideoHelp.UI.Web.Controllers
{
    public class ChatRoomController : Controller
    {
        //
        // GET: /ChatRoom/

        public ActionResult Index()
        {
            return View(UserManager.CurrentUser);
        }

    }
}
