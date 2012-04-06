using System;
using System.Runtime.Serialization;

namespace VideoHelp.Domain.Messages
{
    [DataContract]
    public abstract class DomainCommand
    {
        [DataMember(Order = 1)]
        public readonly Guid AggregateId;

        protected DomainCommand(Guid id)
        {
            AggregateId = id;
        }
    }
}
