using SignalR.Hubs;

namespace VideoHelp.UI.Web.Hubs
{
    public class ConnectUserHub : Hub
    {
        public void Initialize(int userId)
        {
            Caller.Id = userId;
           
        }
    }


    public class TicketHub : Hub
    {
        static int TotalTickets = 10;
        private static int i = 22;
        public void GetTicketCount()
        {
            Caller.id = i++;
            var value = Context.Cookies["Test"];
            Context.Cookies["Test"] = "54st";
            Clients.updateTicketCount(TotalTickets);
            Clients.addMessage("test");
        }

        public void BuyTicket()
        {
            if (TotalTickets > 0)
                TotalTickets -= 1;
            Clients.updateTicketCount(TotalTickets);
        }
    }
}