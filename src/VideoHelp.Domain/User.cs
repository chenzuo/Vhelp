﻿using System;
using System.Collections.Generic;
using CommonDomain.Core;
using VideoHelp.Domain.Messages.Commands;
using VideoHelp.Domain.Messages.Events.Users;

namespace VideoHelp.Domain
{
    public class User : AggregateBase
    {
        public static User Create(Guid guid, string nick, string fullName, string email)
        {
            return new User(guid, nick, fullName, email);
        }
        public User(){}

        private User(Guid guid, string nick, string fullName, string email) : base()
        {
            RaiseEvent(new UserCreated(guid, nick, fullName, email));
        }

        public void AddOwnMeeting(Guid meetingId)
        {
            if(OwnMeetings == null)
            {
                OwnMeetings = new List<Guid>();
            }
            OwnMeetings.Add(meetingId);
        }

        public string Nick { get; private set; }

        public string FullName { get; private set; }

        public string Email { get; private set; }

        public List<string> Identities { get; private set; }

        public UserState State { get; set; }

        public DateTime LastActivity { get; set; }

        public List<Guid> OwnMeetings { get; set; }

        public void AssociatWithIdentity(string identity)
        {
            RaiseEvent(new UserAssociatedWithIdentity(Id, identity));
        }

        public void UpdateState(UserState state, DateTime onDate)
        {
            RaiseEvent(new UserStateUpdated(Id, onDate, state));
        }

        private void Apply(UserCreated userCreated)
        {
            Id = userCreated.AggregateId;
            Nick = userCreated.Nick;
            FullName = userCreated.FullName;
            Email = userCreated.Email;
        }

        private void Apply(UserAssociatedWithIdentity associatedWithIdentity)
        {
            if(Identities == null)
            {
                Identities = new List<string>();
            }
            Identities.Add(associatedWithIdentity.Identity);
        }

        private void Apply(UserStateUpdated @event)
        {
            State = @event.State;
            LastActivity = @event.UpdateDate;
        }
    }
}