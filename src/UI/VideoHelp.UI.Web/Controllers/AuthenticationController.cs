using System;
using System.Web;
using System.Web.Mvc;
using VideoHelp.Domain.Messages.Commands;
using VideoHelp.Infrastructure;
using VideoHelp.ReadModel;
using VideoHelp.ReadModel.Contracts;
using VideoHelp.ReadModel.Users;
using VideoHelp.UI.Domain;
using VideoHelp.UI.Domain.LoginzaAuthentication;
using System.Monads;
using System.Linq;

namespace VideoHelp.UI.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly INotificationBus _notificationBus;
        private readonly ICommandBus _commandBus;
        private readonly AccountInformationExtractor _accountInformationExtractor;

        public AuthenticationController(AccountInformationExtractor accountInformationExtractor, ICommandBus commandBus, IRepositoryFactory repositoryFactory, INotificationBus notificationBus)
        {
            _repositoryFactory = repositoryFactory;
            _notificationBus = notificationBus;
            _commandBus = commandBus.CheckNull("commandBus");
            _accountInformationExtractor = accountInformationExtractor.CheckNull("accountInformationExtractor");
        }

        public ActionResult Logoff()
        {
            _commandBus.Publish(new UpdateUserState(UserManager.CurrentUser, DateTime.Now, UserState.Ofline));
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
            var association = geUsertAssociationWith(account);
            var userId = association.With(view => view.UserId);
            
            if(association == null || association.UserId == Guid.Empty)
            {
               userId = Guid.NewGuid();
               _commandBus.Publish(new CreateUser(userId, account.NickName, account.FullName, account.Email, account.Identity.ToLower()));
               var isUpdated = _notificationBus.WaitNotification<UserView>(userId);
                if(!isUpdated)
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
