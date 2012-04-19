using System;

namespace VideoHelp.ReadModel.Meeting
{
    public class CameraStream 
    {
        public CameraStream(Guid ownerUser, string streamLink)
        {
            OwnerUser = ownerUser;
            StreamLink = streamLink;
        }

        public Guid OwnerUser { get; private set; }
        public string StreamLink { get; private set; }
    }
}