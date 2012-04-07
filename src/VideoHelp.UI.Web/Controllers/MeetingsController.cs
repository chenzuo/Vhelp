using System.Web.Mvc;
using VideoHelp.Domain.Messages.Commands;
using VideoHelp.Infrastructure;
using VideoHelp.ReadModel;
using VideoHelp.ReadModel.Meeting;

namespace VideoHelp.UI.Web.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IReadRepository _repository;

        public MeetingsController(ICommandBus commandBus, IReadRepository repository)
        {
            _commandBus = commandBus;
            _repository = repository;
        }

        //
        // GET: /Meeting/

        public ActionResult Index()
        {
            return View(_repository.GetAll<MeetingListView>());
        }

        public ActionResult CreateMeeting()
        {
            var name = Request.Form["newRoomName"];
            _commandBus.Publish(new CreateMeeting(UserManager.CurrentUser.Id, name));

            return RedirectToAction("Index");
        }
    }
}
