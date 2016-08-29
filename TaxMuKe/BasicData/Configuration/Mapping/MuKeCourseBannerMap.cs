using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Configuration.Mapping
{
    class MuKeCourseBannerMap : EntityTypeConfiguration<MuKeCourseBanner>
    {
        public MuKeCourseBannerMap()
        {
            this.ToTable("muke_coursebanner");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
