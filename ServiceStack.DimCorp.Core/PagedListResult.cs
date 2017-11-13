using System.Collections.Generic;

namespace ServiceStack.DimCorp.Core
{
    public class PagedListResult<TEntity>
    {
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public long Count { get; set; }
        public IList<TEntity> Entities { get; set; }

        public long LastPage()
        {
            var lastPage = Count / Size;
            if (lastPage * Size < Count)
                lastPage++;

            return lastPage;
        }
    }
}
