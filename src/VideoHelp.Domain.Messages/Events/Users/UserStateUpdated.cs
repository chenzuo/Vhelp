using System;
using System.Runtime.Serialization;
using VideoHelp.Domain.Messages.Commands;

namespace VideoHelp.Domain.Messages.Events.Users
{
    [DataContract]
    public class UserStateUpdated : DomainEvent
    {
        public UserStateUpdated(Guid id, DateTime updateDate, UserState state) : base(id)
        {
            UpdateDate = updateDate;
            State = state;
        }

        [DataMember(Order = 2)]
        public DateTime UpdateDate { get; private set; }

        [DataMember(Order = 3)]
        public UserState State { get; private set; }
    }
}