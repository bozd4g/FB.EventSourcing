using System;

namespace FB.EventSourcing.Application.Contracts.Users.Dtos.Request
{
    public class CreateUserRequestDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
    }
}