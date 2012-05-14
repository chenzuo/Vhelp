using System;
using System.Runtime.Serialization;

namespace VideoHelp.Domain.Messages.Events.Users
{
    [DataContract]
    public class UserAssociatedWithIdentity : DomainEvent
    {
        public UserAssociatedWithIdentity(Guid id, string identity, string network) : base(id)
        {
            Identity = identity;
            Network = network;
        }

        [DataMember(Order = 3)]
        public string Identity { get; private set; }

        [DataMember(Order = 4)]
        public string Network { get; private set; }

    }
}