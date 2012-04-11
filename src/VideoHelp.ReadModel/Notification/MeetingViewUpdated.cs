using System.Runtime.Serialization;
using VideoHelp.ReadModel.Meeting;

namespace VideoHelp.ReadModel.Notification
{
    [DataContract]
    public class MeetingViewUpdated : Contracts.Notification
    {
        public MeetingViewUpdated(MeetingView view)
            : base(view.Id)
        {
            View = view;
        }

        [DataMember(Order = 2)]
        public MeetingView View { get; private set; }

    }
}