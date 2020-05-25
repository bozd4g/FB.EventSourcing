using System;
using System.ComponentModel.DataAnnotations.Schema;
using FB.EventSourcing.Domain.SeedWork;
using FB.EventSourcing.Domain.UserAggregate.Events;

namespace FB.EventSourcing.Domain.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string TempEmail { get; private set; }
        public string Password { get; private set; }
        public DateTime? Birthday { get; private set; }
        public string PhoneNumber { get; private set; }
        public GenderType Gender { get; private set; }

        public string About { get; private set; }
        public string PhotoUrl { get; private set; }

        public bool IsEmailConfirmed { get; private set; }
        public bool IsPhoneConfirmed { get; private set; }

        [NotMapped]
        public UserDetail Detail { get; private set; }

        public User()
        {
        }

        public User(string name, string surname, string email, string password, DateTime birtday)
        {
            Name = name;
            Surname = surname;
            Username = $"{name}.{surname}";
            Email = email;
            Password = password;
            Birthday = birtday;
            Detail = new UserDetail(birtday);

            this.AddDomainEvent(new UserCreatedEvent(name, surname, email, password));
        }

        public void RetrieveDetails()
        {
            Detail ??= new UserDetail(this.Birthday.GetValueOrDefault());
        }

        public User ChangeEmail(string email)
        {
            TempEmail = email;
            this.AddDomainEvent(new EmailChangedEvent(email));
            return this;
        }
    }
}