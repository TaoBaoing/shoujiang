using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicUPMS.Models.ViewModels
{
    public class MuKeRecordCourseReport
    {
        public long CourseId { get; set; }

        public string CourseName     { get; set; }

        public string UserName { get; set; }

        public string UserType { get; set; }

        public string ClassOneName { get; set; }
        public string ClassTwoName { get; set; }

        public string IsCache { get; set; }
        public string IsTest { get; set; }
        public int TestScore { get; set; }

    }
}