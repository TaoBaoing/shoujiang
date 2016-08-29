using System;
using System.Collections.Generic;

namespace BasicData
{
    /// <summary>
    /// 接口调用日志，可以在web.config里设置记录日志的开关
    /// </summary>
    public partial class Interfacecalllog
    {
        public long Id { get; set; }
        public int Terminal { get; set; }
        public string TerminalOS { get; set; }
        public int VersionId { get; set; }
        public string VersionName { get; set; }
        public long UserId { get; set; }
        public string UserToken { get; set; }
        public int SecretType { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
