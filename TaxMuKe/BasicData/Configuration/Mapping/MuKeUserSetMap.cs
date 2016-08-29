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
   public  class MuKeUserSetMap : EntityTypeConfiguration<MuKeUserSet>
    {
        public MuKeUserSetMap()
        {
            this.ToTable("muke_user_set");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
