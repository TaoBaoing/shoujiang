using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BasicData.Mapping
{
    public class PermissionRoleMap : EntityTypeConfiguration<PermissionRole>
    {
        public PermissionRoleMap()
        {
            this.ToTable("permission_role");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
