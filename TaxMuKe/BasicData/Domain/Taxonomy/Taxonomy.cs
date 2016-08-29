using System;
using System.Collections.Generic;

namespace BasicData
{
    public partial class Taxonomy
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public TaxonomyTypes TaxonomyType { get; set; }
        public string LinkUrl { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public long ParentId { get; set; }
        public int Depth { get; set; }
        public int Sort { get; set; }
        public int Count { get; set; }
        public MuKeEnabledStatus Status { get; set; }
        public long CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
        public long UpdateUser { get; set; }
        public DateTime? UpdateTime { get; set; }


        //public virtual ICollection<TermTaxonomy> Children { get; set; }
        //public virtual TermTaxonomy Parent { get; set; }

        public virtual ICollection<TaxonomyRelationships> TermRelationships { get; set; }
        public string Ext1 { get; set; }
        public string Ext2 { get; set; }
        public string Ext3 { get; set; }
        public string Ext4 { get; set; }
        public string Ext5 { get; set; }
        public string Ext6 { get; set; }

    }
}
