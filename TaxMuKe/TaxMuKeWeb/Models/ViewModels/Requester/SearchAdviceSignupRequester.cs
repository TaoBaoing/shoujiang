using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicUPMS.Models.ViewModels.Requester
{
    public class SearchAdviceSignupRequester : PagedRequesterBase
    {
        public string name { get; set; }
        public long TagId { get; set; }
    }
}