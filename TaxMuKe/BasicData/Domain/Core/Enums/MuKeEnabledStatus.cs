using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BasicData
{
    public enum MuKeEnabledStatus
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        None = 0,
        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Enabled = 1,
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disabled = 2
    }
}