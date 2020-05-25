using System;

namespace FB.EventSourcing.Domain.Shared
{
    public interface IModificationAudited : IHasModificationTime
    {
        Guid? LastModifierUserId { get; set; }
    }
}