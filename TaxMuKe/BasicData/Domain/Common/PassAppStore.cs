using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Domain.Common
{
    public enum PassAppStore
    {
        None = 0,
        
        [Description("没通过")]
        NotPass = 1,
        
        [Description("通过")]
        Pass = 2,
    }
}
