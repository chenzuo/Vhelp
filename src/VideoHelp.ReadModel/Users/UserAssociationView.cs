using System;
using System.ComponentModel.DataAnnotations;

namespace VideoHelp.ReadModel.Users
{
    public class UserAssociationView
    {
        public UserAssociationView(Guid userId, string identity)
        {
            Identity = identity;
            UserId = userId;
        }

        public UserAssociationView(){}

        public Guid UserId { get; private set; }
        [StringLength(300)]
        public string Identity { get; private set; }
    }
}