using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace FB.EventSourcing.Domain.SeedWork
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? LastModifierUserId { get; set; }


        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        protected void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}