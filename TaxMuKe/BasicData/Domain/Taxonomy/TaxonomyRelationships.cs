using System;
using System.Collections.Generic;

namespace BasicData
{
    public partial class TaxonomyRelationships
    {
        public long Id { get; set; }
        public long ObjectId { get; set; }
        public long TaxonomyId { get; set; }
        public virtual Taxonomy Taxonomy{ get; set; }
        public int TermOrder { get; set; }
        public long CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
