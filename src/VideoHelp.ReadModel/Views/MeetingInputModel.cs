using System;

namespace VideoHelp.ReadModel.Views
{
    public class MeetingInputModel
    {
        public MeetingInputModel(Guid meetingId)
        {
            MeetingId = meetingId;
        }

        public Guid MeetingId { get; set; }
    }
}