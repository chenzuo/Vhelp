﻿using System;
using CommonDomain.Core;
using VideoHelp.Domain.Messages.Events.Meeting;

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


        private void Apply(MeetingCreated @event)
        {
            Id = @event.AggregateId;
            OwnerId = @event.OwnerId;
            Name = @event.Name;
            CreationDate = @event.CreationDate;
        }
    }
}
