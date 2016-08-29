using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicUPMS.Models.ViewModels.Requester
{
    public class MuKeRecordCourseRequest : PagedRequesterBase
    {
        public long UserId { get; set; }
    }
}