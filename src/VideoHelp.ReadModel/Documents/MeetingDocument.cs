using System;
using System.Collections.Generic;

namespace VideoHelp.ReadModel.Documents
{
    public class MeetingDocument : BaseDocument
    {
        public Guid Owner { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public List<WebCameraStream> WebCameraStreams { get; set; }

        public MeetingDocument()
        {
            WebCameraStreams = new List<WebCameraStream>();
        }
    }

    public class WebCameraStream
    {
        public WebCameraStream(Guid ownerUser, string streamSource)
        {
            OwnerUser = ownerUser;
            StreamSource = streamSource;
        }

        public Guid OwnerUser { get; set; }
        public string StreamSource { get; set; }
    }
}