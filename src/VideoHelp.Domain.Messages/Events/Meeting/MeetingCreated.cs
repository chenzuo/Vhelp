using System;
using System.Runtime.Serialization;

namespace VideoHelp.Domain.Messages.Events.Meeting
{
    [DataContract]
    public class MeetingCreated : DomainEvent
    {   
        public MeetingCreated(Guid aggregateId, Guid ownerId, string name, DateTime creationDate) : base(aggregateId)
        {
            Name = name;
            OwnerId = ownerId;
            CreationDate = creationDate;
        }

        [DataMember(Order = 3)]
        public Guid OwnerId { get; private set; }

        [DataMember(Order = 4)]
        public String Name { get; private set; }

        [DataMember(Order = 5)]
        public DateTime CreationDate { get; private set; }
    }
}