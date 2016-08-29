using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BasicData.Mapping
{
    public class TaxonomyMap : EntityTypeConfiguration<Taxonomy>
    {
        public TaxonomyMap()
        {

            this.ToTable("taxonomy");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            //this.HasMany(i => i.Children).WithRequired(i => i.Parent);

        }
    }
}
