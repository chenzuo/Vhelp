using System;

namespace VideoHelp.ReadModel.Contracts
{
    public abstract class Notification
    {
        protected Notification(Guid id)
        {
            NotificationId = id;
        }

        public Guid NotificationId { get; private set; }
    }
}