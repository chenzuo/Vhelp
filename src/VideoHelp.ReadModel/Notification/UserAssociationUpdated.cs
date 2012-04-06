using System;

namespace VideoHelp.ReadModel.Notification
{
    public class UserAssociationUpdated : Contracts.Notification
    {
        public UserAssociationUpdated(Guid id) : base(id)
        {
        }
    }
}