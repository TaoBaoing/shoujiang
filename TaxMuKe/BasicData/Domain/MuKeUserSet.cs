using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Domain
{
   public  class MuKeUserSet
    {
        public long Id { get; set; }

       public long UserId { get; set; }

       public MuKeUserSetType MuKeUserSetType { get; set; }
        
       public string StrValue { get; set; }
       public int IntValue { get; set; }
       public decimal DecimalValue { get; set; }

       public string Remark { get; set; }

       public DateTime CreateTime { get; set; }

    }

    public enum MuKeUserSetType
    {
        [Description("")]
        None = 0,

        [Description("ios推送sound 设置")]
        IosSound = 1,
       

    }
}
