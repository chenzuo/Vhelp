using System;
using System.Runtime.Serialization;
using VideoHelp.Domain.Messages.ValueObject;

namespace VideoHelp.Domain.Messages.Events.Meeting
{
    [DataContract]
    public class MediaContentAdded : DomainEvent
    {
        public MediaContentAdded(Guid meetingId, MediaContent content) : base(meetingId)
        {
           Content = content;
        }

       [DataMember(Order = 3)]
       public MediaContent Content { get; private set; }

    }
}