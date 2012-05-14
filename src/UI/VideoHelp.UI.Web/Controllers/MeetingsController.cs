using System;
using System.Web.Mvc;
using VideoHelp.Domain.Messages.Commands;
using VideoHelp.Infrastructure;
using VideoHelp.ReadModel;
using VideoHelp.ReadModel.Views;

namespace VideoHelp.UI.Web.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly IViewRepository _repository;
        private readonly ICommandBus _commandBus;

        public MeetingsController(IViewRepository repository, ICommandBus commandBus)
        {
            _repository = repository;
            _commandBus = commandBus;
        }

        public ActionResult Index()
        {
            var view = _repository.Load<MeetingBrowseInputModel, MeetingBrowseView>(new MeetingBrowseInputModel(0, 20));
            return View(view);
        }

        public ActionResult Meeting(Guid meetingId)
        {
            var view = _repository.Load<MeetingInputModel, MeetingView>(new MeetingInputModel(meetingId));
            return View(view);
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
