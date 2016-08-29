using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicData.Domain.Course;

namespace BasicData.Configuration.Mapping
{
   public  class MuKeRecordCapterCourseMap : EntityTypeConfiguration<MuKeRecordCourseCapter>
    {
       public MuKeRecordCapterCourseMap()
       {
            this.ToTable("muke_record_course_capter");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
