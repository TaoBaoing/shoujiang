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
    public class MuKeDirtyWordMap : EntityTypeConfiguration<MuKeDirtyWord>
    {
        public MuKeDirtyWordMap()
        {
            this.ToTable("muke_dirtyword");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
