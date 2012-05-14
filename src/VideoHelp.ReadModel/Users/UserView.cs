using System;

namespace VideoHelp.ReadModel.Users
{
    public class UserView : IView
    {
        public UserView(Guid id, string nick, string firstName, string lastName, string email, string network)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Nick = nick;
            Network = network;
        }

        public UserView(){}

        public string Nick { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Network { get; private set; }
        public Guid Id { get; private set; }
    }
}