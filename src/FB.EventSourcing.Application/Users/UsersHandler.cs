using System.Threading;
using System.Threading.Tasks;
using FB.EventSourcing.Application.Contracts.Users;
using FB.EventSourcing.Application.Contracts.Users.Commands;
using FB.EventSourcing.Application.Contracts.Users.Dtos;
using FB.EventSourcing.Application.Contracts.Users.Queries;
using FB.EventSourcing.Application.Exceptions;
using MediatR;

namespace FB.EventSourcing.Application.Users
{
    public class UsersHandler :
        IRequestHandler<GetUserByEmailQuery, UserDto>,
        IRequestHandler<CreateUserCommand, bool>,
        IRequestHandler<ChangeEmailCommand, bool>
    {
        private readonly IUserAppService _userAppService;

        public UsersHandler(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var dto = await _userAppService.GetUser(request);
            return dto;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userAppService.CreateUser(request);
            if (result == null)
                throw new UserFriendlyException("An error has been occurred while creating user. Please try again later.");

            return true;
        }

        public async Task<bool> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _userAppService.ChangeEmail(request);
            if (result == null)
                throw new UserFriendlyException("An error has been occurred while changing email. Please try again later.");

            return true;
        }
    }
}