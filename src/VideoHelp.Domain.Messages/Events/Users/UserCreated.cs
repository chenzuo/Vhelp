using System;
using System.Runtime.Serialization;

namespace VideoHelp.Domain.Messages.Events.Users
{
    [DataContract]
    public class UserCreated : DomainEvent
    {
        public UserCreated(Guid id, string nick, string firstName, string lastName, string email)
            : base(id)
        {
            Nick = nick;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        [DataMember(Order = 3)]
        public string Nick { get; private set; }

        [DataMember(Order = 4)]
        public string FirstName { get; private set; }

        [DataMember(Order = 5)]
        public string LastName { get; private set; }

        [DataMember(Order = 5)]
        public string Email { get; private set; }
    }
}