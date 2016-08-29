using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
    public class CourseViewRecord
    {
        public long Id { get; set; }

        public long UserId { get; set; }
     

        public long CourseId { get; set; }
        public long CourseItemId { get; set; }//对应 capterId

        public int CurrentProcess { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;

    }
}
