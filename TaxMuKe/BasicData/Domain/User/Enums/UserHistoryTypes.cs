using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
    public enum UserHistoryTypes
    {
        None = 0,
        [Description("评星")]
        Ratting = 1,
        [Description("收藏")]
        Favorites = 2,
        [Description("关注")]
        Attention = 3,
        [Description("课程")]
        Course = 4
    }
}
