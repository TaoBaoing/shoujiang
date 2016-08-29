using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BasicData.Mapping
{
    public class UserHistoryMap : EntityTypeConfiguration<UserHistory>
    {
        public UserHistoryMap()
        {
            this.ToTable("user_history");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(t => t.User)
             .WithMany(t => t.Historys)
             .HasForeignKey(d => d.UserId);
        }
    }
}
