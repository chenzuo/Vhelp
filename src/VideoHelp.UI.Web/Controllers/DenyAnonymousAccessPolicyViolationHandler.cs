using System.Web.Mvc;
using FluentSecurity;

namespace VideoHelp.UI.Web.Controllers
{
    public class DenyAnonymousAccessPolicyViolationHandler : IPolicyViolationHandler
    {
        public ActionResult Handle(PolicyViolationException exception)
        {
            return new HttpUnauthorizedResult(exception.Message);
        }
    }
}