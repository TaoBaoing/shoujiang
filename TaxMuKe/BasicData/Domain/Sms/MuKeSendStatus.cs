using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
    /// <summary>
    /// 发送状态
    /// </summary>
    public enum MuKeSendStatus
    {

        /// <summary>
        /// 成功
        /// </summary>
        SendSucceed = 1,

        /// <summary>
        /// 还未发送
        /// </summary>
        NotSend = 2,

        /// <summary>
        /// 失败
        /// </summary>
        SendFail = 3
    }

}
