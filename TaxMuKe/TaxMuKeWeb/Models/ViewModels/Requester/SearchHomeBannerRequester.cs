using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasicData;

namespace BasicUPMS.Models.ViewModels.Requester
{
    public class SearchHomeBannerRequester : PagedRequesterBase
    {
        public MuKeEnabledStatus Status { get; set; }
    }
}