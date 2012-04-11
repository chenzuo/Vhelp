using System;
using System.Runtime.Serialization;

namespace VideoHelp.Domain.Messages.Commands
{
    [DataContract]
    public class CreateCameraStream : DomainCommand
    {
        public CreateCameraStream(Guid meetingId, Guid userId, String streamLink)
            : base(Guid.NewGuid())
        {
            UserId = userId;
            StreamLink = streamLink;
            MeetingId = meetingId;
        }

        [DataMember(Order = 2)]
        public Guid MeetingId { get; set; }

        [DataMember(Order = 3)]
        public Guid UserId { get; set; }

        [DataMember(Order = 4)]
        public string StreamLink { get; set; }
    }
}