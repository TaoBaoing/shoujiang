using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicUPMS.Models.ViewModels
{
    public class MuKeRecordCourseItemReport
    {
        public long CourseId { get; set; }
        public string CourseName { get; set; }
        public long CourseCapterId { get; set; }
        public string CourseCapterName { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string ClassOneName { get; set; }
        public string ClassTwoName { get; set; }

        public string ViewPercent { get; set; }
        public string LastModifyTiem { get; set; }

    }
}