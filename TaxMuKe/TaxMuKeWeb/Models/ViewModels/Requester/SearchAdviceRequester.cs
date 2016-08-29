using BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicUPMS.Models.ViewModels.Requester
{
    public class SearchAdviceRequester : PagedRequesterBase
    {
        public MuKeEnabledStatus Status { get; set; }
        public string Name { get; set; }
        public long CityId { get; set; }
    }
}