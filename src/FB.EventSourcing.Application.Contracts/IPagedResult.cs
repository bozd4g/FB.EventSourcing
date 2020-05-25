namespace FB.EventSourcing.Application.Contracts
{
    public interface IPagedResult<T>: IListResult<T>
    {
        long TotalCount { get; set; }
    }
}