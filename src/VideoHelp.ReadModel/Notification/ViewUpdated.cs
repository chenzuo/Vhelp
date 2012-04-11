using System;
using System.Runtime.Serialization;
using VideoHelp.ReadModel.Contracts;

namespace VideoHelp.ReadModel.Notification
{
    [DataContract]
    public class ViewUpdated<T> : INotification<T> where T : IView
    {
        public ViewUpdated(Guid viewId)
        {
            ViewId = viewId;
        }

        [DataMember(Order = 1)]
        public Guid ViewId { get; private set; }
    }
}