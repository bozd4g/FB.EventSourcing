using System;

namespace FB.EventSourcing.Domain.Shared
{
    public interface IHasModificationTime
    {
        DateTime? LastModificationTime { get; set; }
    }
}