using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BasicData.Mapping
{
    public class VersionManageMap : EntityTypeConfiguration<VersionManage>
    {
        public VersionManageMap()
        {
            // Table & Column Mappings
            this.ToTable("version_manage");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
