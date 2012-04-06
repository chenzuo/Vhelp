using System;
using System.Runtime.Serialization;

namespace VideoHelp.Domain.Messages.Commands
{
    [DataContract]
    public class AddVideoStream : DomainCommand
    {
        public AddVideoStream(Guid meetingId, Guid userId, String streamId)
            : base(meetingId)
        {
            UserId = userId;
            StreamId = streamId;
        }

        [DataMember(Order = 2)]
        public String StreamId { get; private set; }

        [DataMember(Order = 3)]
        public Guid UserId { get; private set; }
    }
}