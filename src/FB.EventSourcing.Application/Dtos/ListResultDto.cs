using System;
using System.Collections.Generic;
using FB.EventSourcing.Application.Contracts;

namespace FB.EventSourcing.Application.Dtos
{
    [Serializable]
    public class ListResultDto<T> : IListResult<T>
    {
        private IReadOnlyList<T> _items;
        public IReadOnlyList<T> Items
        {
            get => _items ??= new List<T>();
            set => _items = value;
        }

        public ListResultDto()
        {
        }

        protected ListResultDto(IReadOnlyList<T> items)
        {
            Items = items;
        }
    }
}