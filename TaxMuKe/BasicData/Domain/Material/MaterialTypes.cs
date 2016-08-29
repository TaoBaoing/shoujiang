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
    public enum MaterialTypes
    {
        None = 0,
        [Description("图片管理")]
        Images = 5,
        [Description("音频管理")]
        Audio = 6,
        [Description("视频管理")]
        Video = 7,
        [Description("其他文件管理")]
        OtherFile = 8,
        [Description("杂志分类")]
        Magazine = 9
    }
}