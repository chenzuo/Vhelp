using System;
using System.Runtime.Serialization;

namespace VideoHelp.Domain.Messages.Commands
{
    [DataContract]
    public class CreateMeeting : DomainCommand
    {
        public CreateMeeting(Guid userId, String name) : base(Guid.NewGuid())
        {
            UserId = userId;
            Name = name;
        }

        [DataMember(Order = 2)]
        public Guid UserId { get; private set; }

        [DataMember(Order = 3)]
        public string Name { get; private set; }
    }
}