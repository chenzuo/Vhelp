﻿using System;
using VideoHelp.Domain.Messages.Events.MediaContent;
using VideoHelp.Domain.Messages.Events.Meeting;
using VideoHelp.ReadModel.Contracts;
using VideoHelp.ReadModel.Notification;

namespace VideoHelp.ReadModel.Meeting
{
    public class MeetingEventHandler
    {
        private readonly IWriteRepository _writeRepository;
        private readonly IReadRepository _readRepository;
        private readonly INotificationBus _notificationBus;

        public MeetingEventHandler(IWriteRepository writeRepository, IReadRepository readRepository, INotificationBus notificationBus)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _notificationBus = notificationBus;
        }

        public void Handle(MeetingCreated @event)
        {
            _writeRepository.Add(new MeetingView(@event.AggregateId, @event.OwnerId, @event.Name, @event.CreationDate));
            _writeRepository.SaveChanges();
        }

        public void Handle(CameraStreamCreated @event)
        {
            var meeting =_readRepository.GetById<MeetingView>(@event.MeetingId);

            meeting.MediaContents.Add( new CameraStream(@event.AggregateId, @event.OwnerUser, @event.StreamLink));
            _readRepository.SaveChanges();
            _notificationBus.PublishNotification(new MeetingViewUpdated(meeting));
        } 
    }

    public class CameraStream : MediaContent
    {
        public CameraStream(Guid id, Guid ownerUser, string streamLink) : base(id, ownerUser)
        {
            StreamLink = streamLink;
        }

        public string StreamLink { get; private set; }
    }
}