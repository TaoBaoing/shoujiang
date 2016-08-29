using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
    public class MuKeCourseBanner
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public long CourseId { get; set; }

        public int Sort { get; set; }
        public DateTime CreateTime { get; set; }

        public MuKeEnabledStatus Status { get; set; }
    }
}
