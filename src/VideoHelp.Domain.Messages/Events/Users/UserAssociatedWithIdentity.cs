using System;
using System.Runtime.Serialization;

namespace VideoHelp.Domain.Messages.Events.Users
{
    [DataContract]
    public class UserAssociatedWithIdentity : DomainEvent
    {
        public UserAssociatedWithIdentity(Guid id, string identity) : base(id)
        {
            Identity = identity;
        }

        [DataMember(Order = 3)]
        public string Identity { get; private set; }
    }
}