using SignalR.Hubs;

namespace VideoHelp.UI.Web.Hubs
{
    public class OnlineUserHub : Hub
    {
        public void Start(int userId)
        {
            Caller.Id = userId;



        //    var response = ServiceClient.Current.Send<OnlineUsersResponse>(new UpdateUserConnectionRequest(userId, Context.ConnectionId));
      //      Clients.updateUserList(response.OnlineUsers);
        }

        public void ConnectTo(int userId, string connectionId)
        {
        }
    }
}