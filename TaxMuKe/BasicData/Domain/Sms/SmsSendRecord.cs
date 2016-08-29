using System;
using System.Collections.Generic;

namespace BasicData
{
    /// <summary>
    /// 短信发送历史
    /// </summary>
    public partial class MuKeSmsSendRecord
    {
        public long Id { get; set; }
        public long ChannelId { get; set; }
        public long RequestId { get; set; }
        public string Telephone { get; set; }
        public string SMSId { get; set; }
        public string ResultCode { get; set; }
        public string ResultMessage { get; set; }

        public string SendContent { get; set; }
        public MuKeSendStatus SendStatus { get; set; }
        public DateTime SendTime { get; set; }
      
    }
}
