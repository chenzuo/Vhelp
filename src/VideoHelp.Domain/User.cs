using System;
using System.Collections.Generic;
using CommonDomain.Core;
using VideoHelp.Domain.Messages.Commands;
using VideoHelp.Domain.Messages.Events.Users;

namespace VideoHelp.Domain
{
    public class User : AggregateBase
    {
        public static User Create(Guid guid, string nick, string firstName, string lastName, string email)
        {
            return new User(guid, nick, firstName, lastName, email);
        }
        public User(){}

        private User(Guid guid, string nick, string firstName, string lastName, string email)
        {
            RaiseEvent(new UserCreated(guid, nick, firstName, lastName, email));
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

        public string FirstName { get; private set; }
        
        public string LastName { get; private set; }

        public string Email { get; private set; }

        public UserState State { get; set; }

        public DateTime LastActivity { get; set; }

        public List<Guid> OwnMeetings { get; set; }

        public void AssociatWithIdentity(string identity, string network)
        {
            RaiseEvent(new UserAssociatedWithIdentity(Id, identity, network));
        }

        public void UpdateState(UserState state, DateTime onDate)
        {
            RaiseEvent(new UserStateUpdated(Id, onDate, state));
        }

        private void Apply(UserCreated userCreated)
        {
            Id = userCreated.AggregateId;
            Nick = userCreated.Nick;
            FirstName = userCreated.FirstName;
            LastName = userCreated.LastName;
            Email = userCreated.Email;
        }

        private void Apply(UserAssociatedWithIdentity associatedWithIdentity)
        {
        }

        private void Apply(UserStateUpdated @event)
        {
            State = @event.State;
            LastActivity = @event.UpdateDate;
        }
    }
}