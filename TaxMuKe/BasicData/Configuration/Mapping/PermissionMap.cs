using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BasicData.Mapping
{
    public class PermissionMap : EntityTypeConfiguration<Permissions>
    {
        public PermissionMap()
        {
            this.ToTable("permissions");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(t => t.PermissionResource)
                .WithMany(t => t.Permissions)
                .HasForeignKey(d => d.PermissionResourceId);
            this.HasRequired(t => t.Role)
                .WithMany(t => t.Permissions)
                .HasForeignKey(d => d.RoleId);

        }
    }
}
