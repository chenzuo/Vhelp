using VideoHelp.ReadModel.Meeting;

namespace VideoHelp.ReadModel.Notification
{
    public class MeetingViewUpdated : Contracts.Notification
    {
        public MeetingViewUpdated(MeetingView view)
            : base(view.Id)
        {
            View = view;
        }

        public MeetingView View { get; private set; }

    }
}