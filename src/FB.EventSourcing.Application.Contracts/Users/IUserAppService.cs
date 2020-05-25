using System.Threading.Tasks;
using FB.EventSourcing.Application.Contracts.Dependency;
using FB.EventSourcing.Application.Contracts.Users.Commands;
using FB.EventSourcing.Application.Contracts.Users.Dtos;
using FB.EventSourcing.Application.Contracts.Users.Queries;
using FB.EventSourcing.Domain.UserAggregate;

namespace FB.EventSourcing.Application.Contracts.Users
{
    public interface IUserAppService : IInstancePerLifetimeScope
    {
        Task<UserDto> GetUser(GetUserByEmailQuery byEmailQuery);

        Task<User> CreateUser(CreateUserCommand command);

        Task<User> ChangeEmail(ChangeEmailCommand command);
    }
}