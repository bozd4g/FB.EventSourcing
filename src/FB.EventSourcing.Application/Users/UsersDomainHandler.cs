using System.Threading;
using System.Threading.Tasks;
using FB.EventSourcing.Domain.UserAggregate.Events;
using MediatR;

namespace FB.EventSourcing.Application.Users
{
    public class UsersDomainHandler :
        INotificationHandler<UserCreatedEvent>,
        INotificationHandler<EmailChangedEvent>
    {
        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            // Send email for completing the registration

            return Task.CompletedTask;
        }

        public Task Handle(EmailChangedEvent notification, CancellationToken cancellationToken)
        {
            // Send email for validation to new email address

            return Task.CompletedTask;
        }
    }
}