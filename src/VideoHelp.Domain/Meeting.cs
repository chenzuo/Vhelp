using System;
using System.Collections.Generic;
using CommonDomain.Core;
using VideoHelp.Domain.Messages.Events.Meeting;
using VideoHelp.Domain.Messages.ValueObject;

namespace VideoHelp.Domain
{
    public class Meeting : AggregateBase
    {
        public static Meeting Create(Guid ownerId, String name)
        {
            return new Meeting(ownerId, name);
        }

        public Meeting() { }

        private Meeting(Guid ownerId, String name)
        {
            RaiseEvent(new MeetingCreated(Guid.NewGuid(), ownerId, name, DateTime.Now.ToUniversalTime()));
        }

        public Guid OwnerId { get; private set; }
  
        public String Name { get; private set; }

        public DateTime CreationDate { get; private set; }

        public List<MediaContent> MediaContents { get; private set; }

        public void AttachMediaContent(MediaContent content)
        {
            RaiseEvent(new MediaContentAdded(Id, content));
        }

        private void Apply(MediaContentAdded @event)
        {
            if(MediaContents == null)
            {
                MediaContents = new List<MediaContent>();
            }

           MediaContents.Add(@event.Content);
        }

        private void Apply(MeetingCreated @event)
        {
            Id = @event.AggregateId;
            OwnerId = @event.OwnerId;
            Name = @event.Name;
            CreationDate = @event.CreationDate;
        }
    }
}
