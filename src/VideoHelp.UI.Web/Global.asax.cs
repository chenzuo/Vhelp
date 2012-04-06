using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FluentSecurity;
using VideoHelp.UI.Utility.BackgroundWorker;
using VideoHelp.UI.Web;

namespace VideoHelp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        private readonly BackgroundWorker _backgroundWorker;

        public MvcApplication()
        {
            _backgroundWorker = new BackgroundWorker();
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
           /* 
              _backgroundWorker
                .AddTask(new BackgroundTask("Debug writer 1", new TimeSpan(0, 1, 0), () => Debug.Write("1. " + DateTime.Now)))
                .AddTask(new BackgroundTask("Online status updater", new TimeSpan(0, 1, 0), updateUserStatus))
                .Start();
            */
            
            GlobalFilters.Filters.Add(new HandleSecurityAttribute(), 0);

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        private static void updateUserStatus()
        {
            if(UserManager.IsUserLogged)
            {
             //   ServiceClient.Current.SendOneWay(new UserActivityRequest(UserManager.CurrentUser.Id));
            }
        }
    }
}