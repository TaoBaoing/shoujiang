using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasicData;

namespace BasicUPMS.Models
{
    public class SearchBannerRequester : PagedRequesterBase
    {
        public MuKeBannerTypes BannerType { get; set; }

        public long ObjectId { get; set; }


    }
}