﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;
using SignalR;
using SignalR.Hosting;
using SignalR.Hubs;
using VideoHelp.Domain.Messages.Commands;
using VideoHelp.Infrastructure;
using VideoHelp.ReadModel;
using VideoHelp.ReadModel.Contracts;
using VideoHelp.ReadModel.Meeting;
using VideoHelp.ReadModel.Notification;
using System.Monads;

namespace VideoHelp.UI.Web.Hubs
{
    public class MeetingHub : Hub, IDisconnect, IConnected
    {
        private readonly ICommandBus _commandBus;
        private readonly INotificationBus _notificationBus;
        private readonly IReadRepository _readRepository;



        public MeetingHub()
        {
            _commandBus = DependencyResolver.Current.GetService<ICommandBus>();
            _notificationBus = DependencyResolver.Current.GetService<INotificationBus>();
            _readRepository = DependencyResolver.Current.GetService<IReadRepository>();


            _notificationBus.SubscribeNotification<MeetingView>(meetingUpdated);
        }

        public void JoinToMeeting(string meetingId, string userId)
        {
           
        }

        private void meetingUpdated(Guid guid)
        {
            //Clients.updateMeeting(notification.View);
        }

        public void AddCameraStream(string meetingId, string userId, string streamLink)
        {
            _commandBus.Publish(new CreateCameraStream(new Guid(meetingId), new Guid(userId), streamLink));
        }

       

        public Task Disconnect()
        {
            Debug.WriteLine("DiСоnnected " + Context.ConnectionId);
            return null;
        }


        public Task Connect()
        {
            Debug.WriteLine("Соnnected " + Context.ConnectionId);
            return null;
        }

        public Task Reconnect(IEnumerable<string> groups)
        {
            //throw new NotImplementedException();
            return null;
        }
    }

    public class UserConnectionIdFactory : IConnectionIdFactory
    {
        public string CreateConnectionId(IRequest request, IPrincipal user)
        {
            return UserManager.CurrentUser.ToString();
        }
    }
}