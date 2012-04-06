using System;
using System.Runtime.Serialization;

namespace VideoHelp.Domain.Messages.Commands
{
    [DataContract]
    public class UpdateUserState : DomainCommand
    {
        public UpdateUserState(Guid id, DateTime updateDate, UserState state) : base(id)
        {
            UpdateDate = updateDate.ToUniversalTime();
            State = state;
        }

        [DataMember(Order = 2)]
        public DateTime UpdateDate { get; private set; }
        
        [DataMember(Order = 3)]
        public UserState State { get; private set; }
    }

    public enum UserState
    {
        Online,
        PublishVideo,
        Ofline,
    }
}