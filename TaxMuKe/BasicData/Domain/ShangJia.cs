using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Domain
{
    public class ShangJia
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string ImageUrl { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public class ShangJiaMap : EntityTypeConfiguration<ShangJia>
    {
        public ShangJiaMap()
        {
            this.ToTable("choujiang_shangjia");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
