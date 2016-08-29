using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasicData;

namespace BasicUPMS.Models
{
    public class SearchMaterialRequester : PagedRequesterBase
    {
        public string Name { get; set; }

        public MaterialTypes MaterialType { get; set; }

        public long TaxonomyId { get; set; }
    }
}