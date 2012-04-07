using System;
using System.Web.Mvc;
using VideoHelp.Domain.Messages.Commands;
using VideoHelp.Infrastructure;
using VideoHelp.ReadModel;
using VideoHelp.ReadModel.Meeting;
using VideoHelp.ReadModel.Users;

namespace VideoHelp.UI.Web.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
       {
       }

        public ActionResult Index()

        {
          //  ViewBag.Users = _repository.GetAll().Select(user => user.Name);
            ViewBag.Message = "test";

            
            return View();
        }

        public ActionResult About()
        {
          
            
            return View(new UserView());
        }



    }
}
