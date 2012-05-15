using System;
using System.Collections.Generic;

namespace VideoHelp.ReadModel.Documents
{
    public class UserDocument : BaseDocument
    {
        public UserDocument()
        {
            AccountAssociations = new List<AccountAssociationDocument>();
        }

        public string Nick { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public List<AccountAssociationDocument> AccountAssociations { get; set; }
    }

    public class AccountAssociationDocument
    {
        public string Identity { get; set; }
        public string Network { get; set; }
    }
}