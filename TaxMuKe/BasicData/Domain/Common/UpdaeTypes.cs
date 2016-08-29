using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{

    public enum UpdaeTypes
    {
        None = 0,
        /// <summary>
        /// 默认替用户下载更新，提示用户进行安装
        /// </summary>
        [Description("提示")]
        Prompt = 1,
        /// <summary>
        /// 默认替用户下载更新，但是不提示用户是否安装
        /// </summary>
        [Description("不提示")]
        NoPrompt = 2,
        /// <summary>
        /// 默认替用户下载更新，强制用户安装
        /// </summary>
        [Description("强制")]
        Force = 3
    }
}
