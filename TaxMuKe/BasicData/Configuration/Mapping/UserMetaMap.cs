using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BasicData.Mapping
{
    public class UserMetaMap : EntityTypeConfiguration<UserMeta>
    {
        public UserMetaMap()
        {
            this.ToTable("user_meta");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(t => t.User)
             .WithMany(t => t.MetaData)
             .HasForeignKey(d => d.UserId);
        }
    }
}
