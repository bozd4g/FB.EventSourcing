using MediatR;

namespace FB.EventSourcing.Domain.UserAggregate.Events
{
    public class EmailChangedEvent : INotification
    {
        public string Email { get; private set; }

        public EmailChangedEvent(string email)
        {
            Email = email;
        }
    }
}