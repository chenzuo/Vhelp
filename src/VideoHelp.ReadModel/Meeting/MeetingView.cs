using System;
using System.Collections.Generic;

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
            VideoStreams = new Dictionary<Guid, string>();
        }

        public Guid Id { get; set; }

        public Guid Owner { get; set; }
        
        public string MainVideoStream { get; set; }

        public IDictionary<Guid, string> VideoStreams { get; set; }
        
        public string Name { get; set; }

        public DateTime CreationDate { get; set; }
    }
}