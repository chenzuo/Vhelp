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
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly ICommandBus _commandBus;

        public MeetingsController(IRepositoryFactory repositoryFactory, ICommandBus commandBus)
        {
            _repositoryFactory = repositoryFactory;
            _commandBus = commandBus;
        }

        public ActionResult Index()
        {
            using (var repository = _repositoryFactory.Create())
            {
                return View(repository.GetAll<MeetingListView>());
            }
        }

        public ActionResult Meeting(Guid meetingId)
        {
            using (var repository = _repositoryFactory.Create())
            {
                return View(repository.GetById<MeetingView>(meetingId));
            }
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
