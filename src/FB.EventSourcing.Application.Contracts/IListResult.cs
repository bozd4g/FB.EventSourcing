using System.Collections.Generic;

namespace FB.EventSourcing.Application.Contracts
{
    public interface IListResult<T>
    {
        IReadOnlyList<T> Items { get; set; }
    }
}