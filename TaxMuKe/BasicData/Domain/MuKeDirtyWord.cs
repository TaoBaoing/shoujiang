using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Domain
{
   public class MuKeDirtyWord//商家
    {
       public long Id { get; set; }

       public string Name { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string ImageUrl { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
