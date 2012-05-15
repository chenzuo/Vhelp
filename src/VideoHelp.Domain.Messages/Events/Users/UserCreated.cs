using System;
using System.Runtime.Serialization;

namespace VideoHelp.Domain.Messages.Events.Users
{
    [DataContract]
    public class UserCreated : DomainEvent
    {
        public UserCreated(Guid id, string nick, string firstName, string lastName, string email, string accountIdentity, string network)
            : base(id)
        {
            Nick = nick;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            AccountIdentity = accountIdentity;
            Network = network;
        }

        [DataMember(Order = 3)]
        public string Nick { get; private set; }

        [DataMember(Order = 4)]
        public string FirstName { get; private set; }

        [DataMember(Order = 5)]
        public string LastName { get; private set; }

        [DataMember(Order = 6)]
        public string Email { get; private set; }

        [DataMember(Order = 7)]
        public string AccountIdentity { get; set; }

        [DataMember(Order = 8)]
        public string Network { get; set; }
    }
}