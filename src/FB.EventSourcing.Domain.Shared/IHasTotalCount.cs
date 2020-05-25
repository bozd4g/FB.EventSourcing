namespace FB.EventSourcing.Domain.Shared
{
    public interface IHasTotalCount
    {
        long TotalCount { get; set; }
    }
}