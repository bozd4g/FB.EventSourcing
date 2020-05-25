using System;
using System.Collections.Generic;
using FB.EventSourcing.Application.Contracts;

namespace FB.EventSourcing.Application.Dtos
{
    [Serializable]
    public class PagedResultDto<T> : ListResultDto<T>, IPagedResult<T>
    {
        public PagedResultDto(IReadOnlyList<T> items) : base(items)
        {
            TotalCount = items.Count;
        }

        public long TotalCount { get; set; }
    }
}