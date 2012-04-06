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
        private readonly ICommandBus _commandBus;
        private readonly IReadRepository _repository;

        public HomeController(ICommandBus bus, IReadRepository repository)
       {
           _commandBus = bus;
           _repository = repository;
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

        public ActionResult Meetings()
        {
            return View(_repository.GetAll<MeetingListView>());
        }

        public ActionResult CreateMeeting()
        {
            //throw new NotImplementedException();
            var name = Request.Form["newRoomName"];
            _commandBus.Publish(new CreateMeeting(UserManager.CurrentUser.Id, name));

            return RedirectToAction("Meetings");
        }
    }
}
