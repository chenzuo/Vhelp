using System.Linq;
using FluentSecurity;

namespace VideoHelp.Helpers
{
    public class SecurityHelper
    {
        public static bool ActionIsAllowedForUser(string controllerName, string actionName)
        {
            var configuration = SecurityConfiguration.Current;
            var policyContainer = configuration.PolicyContainers.GetContainerFor(controllerName, actionName);
            if (policyContainer != null)
            {
                var context = SecurityContext.Current;
                var results = policyContainer.EnforcePolicies(context);
                return results.All(x => x.ViolationOccured == false);
            }
            return true;
        }

        public static bool UserIsAuthenticated()
        {
            var currentUser = SessionContext.CurrentUser;
            return currentUser != null;
        }
    }
}