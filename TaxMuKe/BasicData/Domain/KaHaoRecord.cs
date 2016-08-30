using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Domain
{
   public class KaHaoRecord
    {
        public long Id { get; set; }
        public long KaHaoId { get; set; }
        public virtual  KaHao KaHao { get; set; }

        public long MuKeDirtyWordId { get; set; }//商家ID
        public virtual MuKeDirtyWord MuKeDirtyWord { get; set; }

       public int Lucky { get; set; }//中奖号码

       public DateTime CreateTime { get; set; }

    }
}
