using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasicData;

namespace BasicUPMS.Models
{
    public class SearchRoleRequester : PagedRequesterBase
    {
        public MuKeEnabledStatus Status { get; set; }
        public string Name { get; set; }
    }
}