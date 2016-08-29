using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Domain.Course
{
    public class MuKeCourseSearchKeyWord
    {
        public long Id { get; set; }

        public string KeyWord { get; set; }
        public long SearchCount { get; set; }
        public long ManualCount { get; set; }

    }
}
