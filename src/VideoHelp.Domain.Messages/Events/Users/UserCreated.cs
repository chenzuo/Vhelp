using System;
using System.Runtime.Serialization;

namespace VideoHelp.Domain.Messages.Events.Users
{
    [DataContract]
    public class UserCreated : DomainEvent
    {
        public UserCreated(Guid id, string nick, string fullName, string email) : base(id)
        {
            Nick = nick;
            FullName = fullName;
            Email = email;
        }

        [DataMember(Order = 3)]
        public string Nick { get; private set; }

        [DataMember(Order = 4)]
        public string FullName { get; private set; }

        [DataMember(Order = 5)]
        public string Email { get; private set; }
    }
}