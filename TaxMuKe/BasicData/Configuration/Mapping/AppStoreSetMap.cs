using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Configuration.Mapping
{
    public  class AppStoreSetMap : EntityTypeConfiguration<AppStoreSet>
    {
        public AppStoreSetMap()
        {
            this.ToTable("muke_appstoreset");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
