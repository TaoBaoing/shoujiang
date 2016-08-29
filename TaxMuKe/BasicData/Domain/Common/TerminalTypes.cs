using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Domain.Common
{
    public enum TerminalTypes
    {
        /// <summary>
        /// Android终端操作
        /// </summary>
        Android = 1,
        /// <summary>
        /// IOS终端操作
        /// </summary>
        IOS = 2,
        /// <summary>
        /// WindowsPhone终端操作
        /// </summary>
        WindowsPhone = 3,
        /// <summary>
        /// PC终端操作
        /// </summary>
        PC = 4
    }

    /// <summary>
    /// 操作系统类型
    /// </summary>
    public enum OSTypes
    {
        /// <summary>
        /// Android
        /// </summary>
        Android = 1,
        /// <summary>
        /// IOS
        /// </summary>
        IOS = 2
    }
}
