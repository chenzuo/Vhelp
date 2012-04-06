using System;
using System.Runtime.Serialization;

namespace VideoHelp.Domain.Messages.Events.Meeting
{
    [DataContract]
    public class VideoStreamAdded : DomainEvent
    {
        public VideoStreamAdded(Guid meetingId, Guid userId, string streamId)
            : base(meetingId)
        {
            StreamId = streamId;
            UserId = userId;
        }

        [DataMember(Order = 3)]
        public Guid UserId { get; private set; }

        [DataMember(Order = 4)]
        public string StreamId { get; internal set; }
    }
}