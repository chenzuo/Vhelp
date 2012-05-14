using System;
using System.Runtime.Serialization;
using VideoHelp.ReadModel.Documents;

namespace VideoHelp.ReadModel.Notification
{
    [DataContract]
    public class DocumentUpdated<T> where T : IDocument
    {
        public DocumentUpdated(T document)
        {
            DocumentId = document.Id;
        }

        [DataMember(Order = 1)]
        public Guid DocumentId { get; private set; }
    }
}