using System;
using System.Web;
using System.Web.Mvc;
using VideoHelp.Domain.Messages.Commands;
using VideoHelp.Infrastructure;
using VideoHelp.ReadModel;
using VideoHelp.ReadModel.Contracts;
using VideoHelp.ReadModel.Users;
using System.Monads;
using System.Linq;
using VideoHelp.UI.Utility;
using VideoHelp.UI.Utility.UloginAuthentication;

namespace VideoHelp.UI.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly INotificationBus _notificationBus;
        private readonly ICommandBus _commandBus;

        public AuthenticationController(ICommandBus commandBus, IRepositoryFactory repositoryFactory, INotificationBus notificationBus)
        {
            _repositoryFactory = repositoryFactory;
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
                _commandBus.Publish(new CreateUser(userId, account.NickName, account.FirstName, account.LastName, account.Email, account.Identity.ToLower()));
                var isUpdated = _notificationBus.WaitNotification<UserView>(userId);
                if (!isUpdated)
                {
                    throw new HttpException(500, "Жопа! Такого не должно быть!");
                }
            }

            UserView user;

            using (var repository = _repositoryFactory.Create())
            {
                user = repository.GetById<UserView>(userId);
            }

            _commandBus.Publish(new UpdateUserState(user.Id, DateTime.Now, UserState.Online));
            UserManager.Loggin(user.Id, user.Nick);


            return RedirectToAction("Index", "Meetings");
        }

        private UserAssociationView geUsertAssociationWith(AccountInformation account)
        {
            using (var repository = _repositoryFactory.Create())
            {
                return repository.GetAll<UserAssociationView>().FirstOrDefault(identity => identity.Identity == account.Identity.ToLower());
            }
        }

    }
}
