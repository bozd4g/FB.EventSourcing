using AutoMapper;
using FB.EventSourcing.Domain.UserAggregate;

namespace FB.EventSourcing.Application.Contracts.Users.Dtos
{
    [AutoMap(typeof(User))]
    public class UserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string TempEmail { get; set; }
        public int Age { get; set; }
    }
}