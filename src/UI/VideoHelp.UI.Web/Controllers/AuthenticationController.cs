using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using VideoHelp.Domain.Messages.Commands;
using VideoHelp.Infrastructure;
using VideoHelp.ReadModel;
using VideoHelp.ReadModel.Contracts;
using VideoHelp.ReadModel.Notification;
using VideoHelp.ReadModel.Users;
using VideoHelp.UI.Domain;
using VideoHelp.UI.Domain.LoginzaAuthentication;
using System.Monads;
using System.Linq;

namespace VideoHelp.UI.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IReadRepository _readRepository;
        private readonly INotificationBus _notificationBus;
        private readonly ICommandBus _commandBus;
        private readonly AccountInformationExtractor _accountInformationExtractor;

        public AuthenticationController(AccountInformationExtractor accountInformationExtractor, ICommandBus commandBus, IReadRepository readRepository, INotificationBus notificationBus)
        {
            _readRepository = readRepository;
            _notificationBus = notificationBus;
            _commandBus = commandBus.CheckNull("commandBus");
            _accountInformationExtractor = accountInformationExtractor.CheckNull("accountInformationExtractor");
        }

        public ActionResult Logoff()
        {
            _commandBus.Publish(new UpdateUserState(UserManager.CurrentUser.Id, DateTime.Now, UserState.Ofline));
            UserManager.Logout();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LoginzaLogin(string token)
        {
            var profile = _accountInformationExtractor.Extract(token);

            if (profile == null)
            {
                throw new HttpException(500, "Profile is null");
            }

            return login(profile);
        }

        private ActionResult login(AccountInformation account)
        {
            UserView user;
            var asociation = geUsertAssociationWith(account);

            if(asociation == null || asociation.UserId == Guid.Empty)
            {
                var commandGuid = Guid.NewGuid();
                _commandBus.Publish(new CreateUser(commandGuid, account.NickName, account.FullName, account.Email, account.Identity.ToLower()));
                var notification = _notificationBus.WaitNotification<UserAssociationUpdated>(commandGuid);

                user = _readRepository.GetById<UserView>(notification.NotificationId);
            }
            else
            {
                user = _readRepository.GetAll<UserView>().FirstOrDefault(view => view.Id == asociation.UserId);
            }

            if (user == null)
            {
                throw new HttpException(500, "Жопа! Такого не должно быть!");
            }

            _commandBus.Publish(new UpdateUserState(user.Id, DateTime.Now, UserState.Online));
            UserManager.Loggin(user);
            
            
            return RedirectToAction("Index", "Meetings");
        }

        private UserAssociationView geUsertAssociationWith(AccountInformation account)
        {
            return _readRepository.GetAll<UserAssociationView>().FirstOrDefault(identity => identity.Identity == account.Identity.ToLower());
        }
    }
}
