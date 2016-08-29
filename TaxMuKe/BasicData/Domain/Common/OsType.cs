using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{

    public enum OsType
    {
        None = 0,
        /// <summary>
        /// Android客户端
        /// </summary>
        [Description("Android")]
        Android = 1,
        /// <summary>
        /// IOS客户端
        /// </summary>
        [Description("IOS")]
        Ios = 2,

    }
}
