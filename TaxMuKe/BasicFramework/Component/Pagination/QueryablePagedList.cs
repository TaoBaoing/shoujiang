using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicFramework.Component
{
	 
	[Serializable]
	public class QueryablePagedList<T> :BasePagedList<T>
	{
      
        public QueryablePagedList(IQueryable<T> superset, int pageNumber, int pageSize)
		{
            pageNumber=pageNumber<1?1:pageNumber;
            pageSize = pageSize < 1 ? 1 : pageSize;

			TotalItemCount = superset == null ? 0 : superset.ToList().Count();
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

        //public QueryablePagedList(IEnumerable<T> superset, int pageNumber, int pageSize)
        //    : this(superset.AsQueryable<T>(), pageNumber, pageSize)
        //{
        //}
	}
}