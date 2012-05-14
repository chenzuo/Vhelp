using System;

namespace VideoHelp.ReadModel.Views
{
    public class UserAccoutInputModel
    {
        public UserAccoutInputModel(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}