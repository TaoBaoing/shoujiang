using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BasicData.Mapping
{
    public class RelationshipsMap : EntityTypeConfiguration<TaxonomyRelationships>
    {
        public RelationshipsMap()
        {

            this.ToTable("taxonomy_relationships");
            // Primary Key
            //this.HasKey(t => new { t.ObjectId, t.TaxonomyId });
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(i => i.Taxonomy).WithMany(i => i.TermRelationships).HasForeignKey(i => i.TaxonomyId);
        }
    }
}
