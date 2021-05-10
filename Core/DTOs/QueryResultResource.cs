using System.Collections.Generic;

namespace Kaizen.Core.DTOs
{
    public class QueryResultResource<T>
    {
        public int TotalItems { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}