using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Domain.Course
{
   public   class MuKeRecordCourseCapter
    {
        public long Id { get; set; }
        public long UserId { get; set; }
       public virtual MuKeUsers User { get; set; }
        public long CourseId { get; set; }
        public virtual MuKeCourse Course { get; set; }
        public long CourseCapterId { get; set; }
        public virtual MuKeCourseCapter CourseCapter { get; set; }
        public bool ViewComplete { get; set; }//是否观看完成
        public DateTime CreateTime { get; set; } = DateTime.Now;

        public long CurrentProcess { get; set; }

       public DateTime? LastModifyTime { get; set; }
    }
}
