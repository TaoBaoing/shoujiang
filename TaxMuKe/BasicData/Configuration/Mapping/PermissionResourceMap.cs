using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BasicData.Mapping
{
    public class PermissionResourceMap : EntityTypeConfiguration<PermissionResource>
    {
        public PermissionResourceMap()
        {
            this.ToTable("permission_resource");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //this.HasMany(I => I.Children).WithOptional(I => I.Parent);
        }
    }
}
