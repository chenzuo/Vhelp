using System;

namespace VideoHelp.ReadModel.Meeting
{
    public abstract class MediaContent
    {
        protected MediaContent(Guid id, Guid ownerUser)
        {
            Id = id;
            OwnerUser = ownerUser;
        }

        public Guid Id { get; private set; }
        public Guid OwnerUser { get; private set; }
    }
}