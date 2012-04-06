using System;
using System.Runtime.Serialization;

namespace VideoHelp.Domain.Messages.Commands
{
    [DataContract]
    public class CreateUser : DomainCommand
    {
        public CreateUser(Guid id, string nick, string fullName, string email, string identity) : base(id)
        {
            Nick = nick;
            FullName = fullName;
            Email = email;
            Identity = identity;
        }
        
        [DataMember(Order = 2)]
        public string Nick { get; private set; }

        [DataMember(Order = 3)]
        public string FullName { get; private set; }

        [DataMember(Order = 4)]
        public string Email { get; private set; }

        [DataMember(Order = 5)]
        public string Identity { get; private set; }
    }
}