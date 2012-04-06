using System;
using CommonDomain.Persistence;
using VideoHelp.Domain.Messages.Commands;

namespace VideoHelp.Domain.CommandHandlers
{
    public class UserCommandHandler : ICommandHandler<CreateUser>, ICommandHandler<UpdateUserState>, ICommandHandler<CreateMeeting>
    {
        private readonly IRepository _repository;

        public UserCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CreateUser command)
        {
            var user = User.Create(command.AggregateId, command.Nick, command.FullName, command.Email);
            _repository.Save(user, Guid.NewGuid());

            user.AssociatWithIdentity(command.Identity);
            _repository.Save(user, Guid.NewGuid());
        }

        public void Handle(UpdateUserState command)
        {
            var user = _repository.GetById<User>(command.AggregateId, int.MaxValue);
            if (user == null)
                throw new OperationCanceledException(string.Format("User with id {0} not found", command.AggregateId));

            user.UpdateState(command.State, command.UpdateDate);
            _repository.Save(user, Guid.NewGuid());
        }

        public void Handle(CreateMeeting command)
        {
            var user = _repository.GetById<User>(command.UserId, int.MaxValue);
            if (user == null)
                throw new OperationCanceledException(string.Format("User with id {0} not found", command.AggregateId));

            var meeting = Meeting.Create(command.UserId, command.Name); 
            _repository.Save(meeting, Guid.NewGuid());

            user.AddOwnMeeting(meeting.Id);
            _repository.Save(user, Guid.NewGuid());
        }
    }
}
