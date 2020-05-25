namespace FB.EventSourcing.Domain.Shared
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}