﻿using BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicUPMS.Models.ViewModels.Requester
{
    public class SearchCityRequester : PagedRequesterBase
    {
        public MuKeEnabledStatus Status { get; set; }
    }
}