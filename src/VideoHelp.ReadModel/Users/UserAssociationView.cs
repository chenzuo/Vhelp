using System;

namespace VideoHelp.ReadModel.Users
{
    public class UserAssociationView : IView
    {
        public UserAssociationView(Guid userId, string identity)
        {
            Identity = identity;
            UserId = userId;
        }

        public UserAssociationView(){}

        public Guid UserId { get; private set; }
      
        public string Identity { get; private set; }
    }
}