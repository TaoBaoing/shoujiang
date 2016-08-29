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
    public class MuKeHomeCourseSetMap : EntityTypeConfiguration<MuKeHomeCourseSet>
    {
        public MuKeHomeCourseSetMap()
        {
            this.ToTable("muek_home_course_set");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
