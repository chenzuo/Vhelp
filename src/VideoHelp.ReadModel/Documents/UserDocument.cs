using System;
using System.Collections.Generic;

namespace VideoHelp.ReadModel.Documents
{
    public class UserDocument : IDocument
    {
        public UserDocument(Guid id, string nick, string firstName, string lastName, string email)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Nick = nick;
        }

        public UserDocument() { }

        public Guid Id { get; set; }

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