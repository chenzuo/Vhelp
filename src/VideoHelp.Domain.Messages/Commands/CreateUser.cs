using System;
using System.Runtime.Serialization;

namespace VideoHelp.Domain.Messages.Commands
{
    [DataContract]
    public class CreateUser : DomainCommand
    {
        public CreateUser(Guid userId, string nick, string firstName, string lastName, string email, string network, string identity) : base(userId)
        {
            Nick = nick;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Identity = identity;
            Network = network;
        }
        
        [DataMember(Order = 2)]
        public string Nick { get; private set; }

        [DataMember(Order = 3)]
        public string FirstName { get; private set; }

        [DataMember(Order = 4)]
        public string LastName { get; private set; }

        [DataMember(Order = 5)]
        public string Email { get; private set; }

        [DataMember(Order = 5)]
        public string Identity { get; private set; }

        [DataMember(Order = 6)]
        public string Network { get; private set; }
    }
}