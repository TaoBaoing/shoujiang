using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicData.Domain.Course;

namespace BasicData.Configuration.Mapping
{
    public class MuKeCourseSearchKeyWordMap : EntityTypeConfiguration<MuKeCourseSearchKeyWord>
    {
        public MuKeCourseSearchKeyWordMap()
        {
            this.ToTable("muke_course_search_keyword");
        }
    }
}
