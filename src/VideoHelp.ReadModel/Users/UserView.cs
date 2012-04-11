using System;
using VideoHelp.ReadModel.Contracts;

namespace VideoHelp.ReadModel.Users
{
    public class UserView : IView
    {
        public UserView(Guid id, string nick, string fullName, string email)
        {
            Id = id;
            Email = email;
            FullName = fullName;
            Nick = nick;
        }

        public UserView(){}

        public string Nick { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public Guid Id { get; private set; }
    }
}