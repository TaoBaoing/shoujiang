using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
    public enum MuKeUserTypes
    {
        [Description("全部")]
        None = 0,

        [Description("未开通会员")]
        FreeUser = 1,

        [Description("E金会员")]
        GoldUser = 2,

        [Description("E尊会员")]
        WhiteGoldUser = 3,

//        [Description("老师")]
//        WhiteGoldUser = 3,
    }
}
