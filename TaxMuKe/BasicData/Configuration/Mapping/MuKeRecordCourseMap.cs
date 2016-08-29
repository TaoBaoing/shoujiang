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
    public class MuKeRecordCourseMap : EntityTypeConfiguration<MuKeRecordCourse>
    {
        public MuKeRecordCourseMap()
        {
            this.ToTable("muke_record_course");
            this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CreateTime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
