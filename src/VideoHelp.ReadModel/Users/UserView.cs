using System;
using VideoHelp.ReadModel.Contracts;

namespace VideoHelp.ReadModel.Users
{
    public class UserView : IView
    {
        public UserView(Guid id, string nick, string firstName, string lastName, string email)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Nick = nick;
        }

        public UserView(){}

        public string Nick { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public Guid Id { get; private set; }
    }
}