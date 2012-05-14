using System;

namespace VideoHelp.ReadModel.Views
{
    public class MeetingBrowseItem
    {
        public MeetingBrowseItem(Guid meetingId, string ownerNick, string name, DateTime creationDate)
        {
            Id = meetingId;
            OwnerNick = ownerNick;
            Name = name;
            CreationDate = creationDate;
        }

        public Guid Id { get; set; }
        
        public string OwnerNick { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }
    }
}