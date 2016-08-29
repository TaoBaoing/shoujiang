using System;
using System.Collections.Generic;

namespace BasicFramework.Component
{
	[Serializable]
	public class StaticPagedList<T> : BasePagedList<T>
	{
		public StaticPagedList(IEnumerable<T> subset, IPagedList metaData)
			: this(subset, metaData.PageNumber, metaData.PageSize, metaData.TotalItemCount)
		{
		}

		public StaticPagedList(IEnumerable<T> subset, int pageNumber, int pageSize, int totalItemCount)
			: base(subset,pageNumber, pageSize, totalItemCount)
		{
			//Subset.AddRange(subset);
		}
	}
}