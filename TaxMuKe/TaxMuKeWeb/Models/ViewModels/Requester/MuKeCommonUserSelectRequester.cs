using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicUPMS.Models.ViewModels.Requester
{
    public class MuKeCommonUserSelectRequester: PagedRequesterBase
    {
        public string UserName  { get; set; }

        public string Phone { get; set; }
    }
}