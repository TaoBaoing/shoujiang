using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasicCommon;

namespace BasicUPMS.Models
{
    public class PagedRequesterBase
    {
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; }

        private int _pageSize;
        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize
        {
            get { return _pageSize == 0 ? UPMSConfig.DefaultPageSize : _pageSize; }
            set { _pageSize = value; }
        }
    }
}