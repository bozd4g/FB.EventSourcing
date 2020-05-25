
namespace FB.EventSourcing.Application.Contracts.Users.Dtos.Request
{
    public class AuthenticateRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}