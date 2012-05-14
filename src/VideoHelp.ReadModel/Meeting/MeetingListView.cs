using System;

namespace VideoHelp.ReadModel.Meeting
{
    public class MeetingListView : IView
    {
        public MeetingListView(Guid id, Guid ownerId, string ownerNick, string name, DateTime creationDate)
        {
            Id = id;
            OwnerId = ownerId;
            OwnerNick = ownerNick;
            Name = name;
            CreationDate = creationDate;
        }

        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }
        
        public string OwnerNick { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }
    }
}