using System;
using System.Web.Mvc;
using VideoHelp.Domain.Messages.Commands;
using VideoHelp.Infrastructure;
using VideoHelp.ReadModel;
using VideoHelp.ReadModel.Meeting;

namespace VideoHelp.UI.Web.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly IReadRepository _repository;
        private readonly ICommandBus _commandBus;

        public MeetingsController(IReadRepository repository, ICommandBus commandBus)
        {
            _repository = repository;
            _commandBus = commandBus;
        }

        public ActionResult Index()
        {
            return View(_repository.GetAll<MeetingListView>());
        }

        public ActionResult Meeting(Guid meetingId)
        {
            var meeting = _repository.GetById<MeetingView>(meetingId);
            return View(meeting);
        }

        [HttpPost]
        public ActionResult NewMeeting()
        {
            var meetingName = Request.Form["meetingName"];
            _commandBus.Publish(new CreateMeeting(UserManager.CurrentUser, meetingName));
            return RedirectToAction("Index");
        }
    }
}
