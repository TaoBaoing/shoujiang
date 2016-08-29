using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
   public class MuKeCourseCapterTestPagperManager
    {
       public MuKeCourseCapterTestPagperManager()
       {
            MuKeCourseCapterTestPagpers=new List<MuKeCourseCapterTestPagper>();
       }

       public long Id { get; set; }

        public long CourseId { get; set; }
        public virtual MuKeCourse Course { get; set; }

        public int TestId { get; set; }//第几套题

        public MuKeEnabledStatus Status { get; set; }//

        public virtual List<MuKeCourseCapterTestPagper> MuKeCourseCapterTestPagpers { get; set; }

    }
}
