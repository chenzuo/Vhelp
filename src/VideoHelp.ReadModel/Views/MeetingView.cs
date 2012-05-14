using System;
using System.Collections.Generic;
using VideoHelp.ReadModel.Documents;

namespace VideoHelp.ReadModel.Views
{
    public class MeetingView
    {
        public MeetingView(Guid id, string name, List<WebCameraStream> webCameraStreams)
        {
            Id = id;
            Name = name;
            WebCameraStreams = webCameraStreams;
        }

        public Guid Id { get; set; }
        public string Name { get; private set; }
        public List<WebCameraStream> WebCameraStreams { get; private set; }
    }
}