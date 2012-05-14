using System;
using System.Web;
using System.Web.Mvc;
using VideoHelp.Domain.Messages.Commands;
using VideoHelp.Infrastructure;
using VideoHelp.ReadModel;
using System.Monads;
using System.Linq;
using VideoHelp.ReadModel.Documents;
using VideoHelp.ReadModel.Views;
using VideoHelp.UI.Utility;
using VideoHelp.UI.Utility.UloginAuthentication;

namespace VideoHelp.UI.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IViewRepository _viewRepository;
        private readonly INotificationBus _notificationBus;
        private readonly ICommandBus _commandBus;

        public AuthenticationController(ICommandBus commandBus, IViewRepository viewRepository, INotificationBus notificationBus)
        {
            _viewRepository = viewRepository;
            _notificationBus = notificationBus;
            _commandBus = commandBus.CheckNull("commandBus");
        }

        public ActionResult Logoff()
        {
            _commandBus.Publish(new UpdateUserState(UserManager.CurrentUser, DateTime.Now, UserState.Ofline));
            UserManager.Logout();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ULoginLogin(string token)
        {
            var accountInfo = new UloginAccountInformationExtractor(token, Request.ServerVariables["SERVER_NAME"]).Extract();

            var account = new AccountInformation
                    {
                        NickName = accountInfo.nickname,
                        FirstName = accountInfo.first_name,
                        LastName = accountInfo.last_name,
                        Email = accountInfo.email,
                        Identity = accountInfo.identity,
                        Network = accountInfo.network,
                    };

            return login(account);
        }

        private ActionResult login(AccountInformation account)
        {
            var association = geUsertAssociationWith(account);
            var userId = association.With(view => view.UserId);

            if (association == null || association.UserId == Guid.Empty)
            {
                userId = Guid.NewGuid();
                _commandBus.Publish(new CreateUser(userId, account.NickName, account.FirstName, account.LastName, account.Email, account.Network, account.Identity.ToLower()));
                var isUpdated = _notificationBus.WaitNotification<UserDocument>(userId);
                if (!isUpdated)
                {
                    throw new HttpException(500, "Жопа! Такого не должно быть!");
                }
            }

            UserAccoutView user = _viewRepository.Load<UserAccoutInputModel, UserAccoutView>(new UserAccoutInputModel(userId));
            
            _commandBus.Publish(new UpdateUserState(user.UserId, DateTime.Now, UserState.Online));
            UserManager.Loggin(user.UserId, user.Nick);


            return RedirectToAction("Index", "Meetings");
        }

        private AccountAssociationView geUsertAssociationWith(AccountInformation account)
        {
            return _viewRepository.Load<AccountAssociationInputModel, AccountAssociationView>(new AccountAssociationInputModel(account.Identity));
        }

    }
}
