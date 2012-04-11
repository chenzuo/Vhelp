using System;
using System.Runtime.Serialization;

namespace VideoHelp.ReadModel.Notification
{
    [DataContract]
    public class UserAssociationUpdated : Contracts.Notification
    {
        public UserAssociationUpdated(Guid id) : base(id)
        {
        }
    }
}