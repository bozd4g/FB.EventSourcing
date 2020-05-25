using System;
using System.Linq;
using System.Threading.Tasks;
using FB.EventSourcing.Domain.SeedWork;
using MediatR;
using Newtonsoft.Json;

namespace FB.EventSourcing.Persistence.EntityFrameworkCore
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, ApplicationDbContext context)
        {
            // We can use Message Broker for events.
            // I used only MediatR

            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());
            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
                Console.WriteLine($"\n------\nA domain event has been published!\n" +
                                  $"Event: {domainEvent.GetType().Name}\n" +
                                  $"Data: {JsonConvert.SerializeObject(domainEvent)}\n------\n");
            }
        }
    }
}