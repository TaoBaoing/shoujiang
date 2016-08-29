using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Domain.Course
{
    public class MuKeRecordCourse
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public virtual MuKeUsers User { get; set; }
        public long CourseId { get; set; }
        public virtual MuKeCourse Course { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
