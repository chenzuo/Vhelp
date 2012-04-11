using System;
using System.Runtime.Serialization;

namespace VideoHelp.Domain.Messages.Events.MediaContent
{
    [DataContract]
    public class CameraStreamCreated : DomainEvent
    {
        public CameraStreamCreated(Guid id, Guid meetingId, Guid ownerUser, string streamLink) : base(id)
        {
            MeetingId = meetingId;
            OwnerUser = ownerUser;
            StreamLink = streamLink;
        }

        [DataMember(Order = 3)]
        public Guid MeetingId { get; private set; }

        [DataMember(Order = 4)]
        public Guid OwnerUser { get; private set; }

        [DataMember(Order = 5)]
        public string StreamLink { get; private set; }
    }
}