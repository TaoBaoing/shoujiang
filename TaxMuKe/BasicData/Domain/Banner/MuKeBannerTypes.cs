using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BasicData
{
    /// <summary>
    /// 轮播图类型
    /// </summary>
    public enum MuKeBannerTypes
    {

        [Description("全部")]
        None = 0,
        [Description("首页")]
        Home = 1,
        [Description("课程中心")]
        Course = 2
        
    }
}