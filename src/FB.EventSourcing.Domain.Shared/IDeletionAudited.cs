using System;

namespace FB.EventSourcing.Domain.Shared
{
    public interface IDeletionAudited : ISoftDelete
    {
        Guid? DeleterUserId { get; set; }
        DateTime? DeletionTime { get; set; }
    }
}