using System;

namespace FB.EventSourcing.Domain.Shared
{
    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }
}