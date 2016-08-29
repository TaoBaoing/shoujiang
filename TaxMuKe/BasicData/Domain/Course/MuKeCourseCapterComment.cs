using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Domain.Course
{
    // 对课程评论，非章节评论，原来是对章节评论的
    public class MuKeCourseCapterComment
    {
        public long Id { get; set; }
        public long UserId { get; set; }

        public virtual  MuKeUsers User { get; set; }
        public long CourseId { get; set; }
        public long CourseCapterId { get; set; }
        public string Comment { get; set; }//评论内容
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
