using System;

namespace VideoHelp.ReadModel.Meeting
{
    public class MeetingView : IView
    {
        public MeetingView(Guid id, Guid owner, String name, DateTime creationDate)
        {
            Id = id;
            Owner = owner;
            Name = name;
            CreationDate = creationDate;
        }

        public Guid Id { get; set; }

        public Guid Owner { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }
    }
}