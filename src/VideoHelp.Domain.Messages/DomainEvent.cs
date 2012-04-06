using System;
using System.Runtime.Serialization;

namespace VideoHelp.Domain.Messages
{
    /// <summary>
    /// Denotes an event in the domain model.
    /// </summary>
    [DataContract]
    public class DomainEvent
    {
        public DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }

        /// <summary>
        /// Gets the aggregate root id of the domain event.
        /// </summary>
        [DataMember(Order = 1)]
        public Guid AggregateId { get; set; }

        /// <summary>
        /// Gets the version of the aggregate which this event corresponds to.
        /// E.g. CreateNewCustomerCommand would map to (:NewCustomerCreated).Version = 1,
        /// as that event corresponds to the creation of the customer.
        /// </summary>
        [DataMember(Order = 2)]
        public uint Version { get; set; }
    }
}