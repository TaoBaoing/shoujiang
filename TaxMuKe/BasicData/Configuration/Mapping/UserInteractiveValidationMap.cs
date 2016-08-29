using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BasicData.Mapping
{
    public class UserInteractiveValidationMap : EntityTypeConfiguration<MuKeUserInteractiveValidation>
    {
        public UserInteractiveValidationMap()
        {
            // Table & Column Mappings
            this.ToTable("muke_user_interactive_validation");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
