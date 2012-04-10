using System;
using System.Runtime.Serialization;
using VideoHelp.Domain.Messages.ValueObject;

namespace VideoHelp.Domain.Messages.Commands
{
    [DataContract]
    public class AttachMediaContent : DomainCommand
    {
        public AttachMediaContent(Guid meetingId, MediaContent content)
            : base(meetingId)
        {
            Content = content;
        }

        [DataMember(Order = 2)]
        public MediaContent Content { get; set; }
    }
}