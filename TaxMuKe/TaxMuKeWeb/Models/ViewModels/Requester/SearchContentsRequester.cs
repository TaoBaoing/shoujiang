using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasicData;

namespace BasicUPMS.Models
{
    public class SearchContentsRequester : PagedRequesterBase
    {
      
        public string UserName { get; set; }
//        public ContentTypes ContentType { get; set; }

        public string Title { get; set; }

        public MuKeEnabledStatus Status { get; set; }
        public MuKeEnabledStatus RecommendStatus { get; set; }
        public MuKeEnabledStatus HotStatus { get; set; }
        public MuKeEnabledStatus NewStatus { get; set; }
        public MuKeCourseNewNoticeType MuKeCourseNewNoticeType { get; set; } 


        public long TaxonomyId { get; set; }

        public long ObjectId { get; set; }
        public long CourseId { get; set; }
    }
}