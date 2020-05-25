using System;

namespace FB.EventSourcing.Domain.Shared
{
    public interface IHasCreationTime
    {
        DateTime CreationTime { get; set; }
    }
}