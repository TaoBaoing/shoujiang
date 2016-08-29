using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{

    public enum SeriesType
    {
        [Description("全部")]
        None = 0,
        [Description("单个")]
        News = 1,

        [Description("系列")]
        Teacher = 2
    }
    public enum ItemContentType
    {
        [Description("全部")]
        None = 0,

        [Description("视频")]
        Video = 1,

        [Description("图文")]
        TextPicture = 2
    }

    public enum CourseCategory
    {
        [Description("全部")]
        None = 0,
        [Description("金融")]
        Finance = 1,

        [Description("互联网")]
        Internet = 2
    }

    public enum CourseType
    {
        [Description("全部")]
        None = 0,

        [Description("免费")]
        FreeUser = 1,

        [Description("收费")]
        ChargeUser = 2,
        
        [Description("未配置")]
        Unknown = 4,
    }

    public enum MuKeCourseDragType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        None = 0,
        /// <summary>
        /// 启用
        /// </summary>
        [Description("是")]
        Allow = 1,
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("否")]
        NotAllow = 2
    }
    public enum MuKeCourseCacheType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        None = 0,
        /// <summary>
        /// 启用
        /// </summary>
        [Description("是")]
        Allow = 1,
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("否")]
        NotAllow = 2
    }

    /// <summary>
    /// 新课程是否已经推送过
    /// </summary>
    public enum MuKeCourseNewNoticeType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        None = 0,

        /// <summary>
        /// 未推送
        /// </summary>
        [Description("未推送")]
        NotPush = 1,

        /// <summary>
        /// 已推送
        /// </summary>
        [Description("已推送")]
        AlreadyPush = 2,
       
    }
}
