using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BasicFramework.Component
{

    [Serializable]
    public abstract class BasePagedList<T> : PagedListMetaData, IPagedList<T>
    {

        protected readonly List<T> Subset = new List<T>();

        protected internal BasePagedList()
        {
        }

    
        protected internal BasePagedList(IEnumerable<T> superset, int pageNumber, int pageSize, int totalItemCount)
        {
            if (pageNumber <= 1)
            {
                pageNumber = 1;
//                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "页码必须大于1");
            }
            
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "每页数量必须大于1");

            TotalItemCount = totalItemCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
            PageCount = TotalItemCount > 0
                            ? (int)Math.Ceiling(TotalItemCount / (double)PageSize)
                            : 0;
            HasPreviousPage = PageNumber > 1;
            HasNextPage = PageNumber < PageCount;
            IsFirstPage = PageNumber == 1;
            IsLastPage = PageNumber >= PageCount;
            FirstItemOnPage = (PageNumber - 1) * PageSize + 1;
            var numberOfLastItemOnPage = FirstItemOnPage + PageSize - 1;
            LastItemOnPage = numberOfLastItemOnPage > TotalItemCount
                                ? TotalItemCount
                                : numberOfLastItemOnPage;


            if (superset != null && TotalItemCount > 0)
                Subset.AddRange(pageNumber == 1
                    ? superset.Skip(0).Take(pageSize).ToList()
                    : superset.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()
                );
        }

        #region IPagedList<T> Members


        public IEnumerator<T> GetEnumerator()
        {
            return Subset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T this[int index]
        {
            get { return Subset[index]; }
        }

        public int Count
        {
            get { return Subset.Count; }
        }

        public bool HasItems
        {
            get { return Subset.Count > 0; }
        }

        public IPagedList GetMetaData()
        {
            return new PagedListMetaData(this);
        }

        #endregion
    }
}