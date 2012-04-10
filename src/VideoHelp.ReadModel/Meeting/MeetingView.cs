using System;
using System.Collections.Generic;
using VideoHelp.Domain.Messages.ValueObject;

namespace VideoHelp.ReadModel.Meeting
{
    public class MeetingView
    {
        public MeetingView(Guid id, Guid owner, String name, DateTime creationDate)
        {
            Id = id;
            Owner = owner;
            Name = name;
            CreationDate = creationDate;
            MediaContents = new List<MediaContent>();
        }

        public Guid Id { get; set; }

        public Guid Owner { get; set; }
        
        public IList<MediaContent> MediaContents { get; set; }
        
        public string Name { get; set; }

        public DateTime CreationDate { get; set; }
    }
}