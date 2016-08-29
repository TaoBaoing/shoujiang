using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Domain.Course
{
    public class MuKeCourseTestRecord
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public virtual MuKeUsers User { get; set; }

        public long MuKeCourseId { get; set; }
        public virtual MuKeCourse MuKeCourse { get; set; }
        public long MuKeCourseCapterTestPagperManagerId { get; set; }
        public virtual MuKeCourseCapterTestPagperManager MuKeCourseCapterTestPagperManager { get; set; }
        public int GetCorce { get; set; }//得分

        public DateTime CreateTime { get; set; }

        public string TestResultJson { get; set; }//考试回传json
    }
}
