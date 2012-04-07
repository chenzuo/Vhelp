using System.Web.Mvc;
using VideoHelp.ReadModel;
using VideoHelp.ReadModel.Meeting;

namespace VideoHelp.UI.Web.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly IReadRepository _repository;
        
        public MeetingsController(IReadRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View(_repository.GetAll<MeetingListView>());
        }

    }
}
