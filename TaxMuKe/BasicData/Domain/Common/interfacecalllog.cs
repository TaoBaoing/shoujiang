using System;
using System.Collections.Generic;

namespace BasicData
{
    /// <summary>
    /// �ӿڵ�����־��������web.config�����ü�¼��־�Ŀ���
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
