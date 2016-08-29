using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BasicData.Mapping
{
    public class BannerMap : EntityTypeConfiguration<MuKeBanner>
    {
        /// <summary>
        /// �ֲ�ͼ
        /// </summary>
        public BannerMap()
        {
            this.ToTable("muke_banner");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
