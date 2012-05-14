using System;
using System.Collections.Generic;

namespace VideoHelp.ReadModel.Meeting
{
    public class MeetingCameraStreamsView : IView
    {
        public MeetingCameraStreamsView(Guid meetingId)
        {
            Id = meetingId;
            CameraStreams = new List<CameraStream>();
        }

        public Guid Id { get; private set; }

        public IList<CameraStream> CameraStreams { get; private set; }
    }
}