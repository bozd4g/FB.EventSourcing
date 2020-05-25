using System;

namespace FB.EventSourcing.Domain.Shared
{
    public interface ICreationAudited : IHasCreationTime
    {
        Guid? CreatorUserId { get; set; }
    }
}