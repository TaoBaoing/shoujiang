using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BasicData.Mapping
{
    public class SettingsMap : EntityTypeConfiguration<Settings>
    {
        /// <summary>
        /// »´æ÷…Ë÷√
        /// </summary>
        public SettingsMap()
        {
            // Table & Column Mappings
            this.ToTable("settings");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
