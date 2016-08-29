using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
    /// <summary>
    /// 平台类型
    /// </summary>
    public enum MuKeSmsSourceTypes
    {
        None = 0,
        [Description("燕园财税")]
        TaxMuKe = 1,
        [Description("其他")]
        Other = 2
    }
}
