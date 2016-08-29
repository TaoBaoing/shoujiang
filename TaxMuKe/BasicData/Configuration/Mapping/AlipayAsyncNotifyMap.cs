using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicData.Domain;

namespace BasicData.Configuration.Mapping
{
   public  class AlipayAsyncNotifyMap : EntityTypeConfiguration<AlipayAsyncNotifyLog>
    {
        public AlipayAsyncNotifyMap()
        {
            this.ToTable("muke_alipay_asyncnotify_log");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
