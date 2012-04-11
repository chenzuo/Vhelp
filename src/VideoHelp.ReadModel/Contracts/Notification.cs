using System;
using System.Runtime.Serialization;

namespace VideoHelp.ReadModel.Contracts
{
    [DataContract]
    public abstract class Notification
    {
        protected Notification(Guid id)
        {
            NotificationId = id;
        }

        [DataMember(Order = 1)]
        public Guid NotificationId { get; private set; }
    }
}