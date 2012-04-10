using System;
using System.Runtime.Serialization;

namespace VideoHelp.Domain.Messages.ValueObject
{
    public class CameraStream : MediaContent
    {
        public CameraStream(Guid id, Guid ownerUser, string streamLink) : base(id, ownerUser)
        {
            StreamLink = streamLink;
        }

        [DataMember]
        public String StreamLink { get; private set; }
    }
}