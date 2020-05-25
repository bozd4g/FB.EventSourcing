using FB.EventSourcing.Application.Contracts.Users.Dtos;
using MediatR;

namespace FB.EventSourcing.Application.Contracts.Users.Queries
{
    public class GetUserByEmailQuery : IRequest<UserDto>
    {
        public string Email { get; private set; }

        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
    }
}