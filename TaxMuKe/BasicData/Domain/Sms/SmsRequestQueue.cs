using System;
using System.Collections.Generic;

namespace BasicData
{
    /// <summary>
    /// 短信请求队列
    /// </summary>
    public partial class SmsRequestQueue
    {
        public long Id { get; set; }

        public string Telephone { get; set; }
        public string TextContent { get; set; }
        public byte[] BinaryContent { get; set; }
        public DateTime PlanningTime { get; set; }
        public MuKeSmsSourceTypes SourceType { get; set; }
        public DateTime SendTime { get; set; }
        public MuKeSendStatus SendStatus { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
