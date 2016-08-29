using System;
using System.Collections.Generic;
using BasicData.Domain.Common;

namespace BasicData
{
    /// <summary>
    /// ∞Ê±æπ‹¿Ì
    /// </summary>
    public partial class VersionManage
    {
     
        public long Id { get; set; }
        public int AppId { get; set; }
        public string AppName { get; set; }
        public OsType OS { get; set; }
        public string OSVersion { get; set; }
        public string AndroidVersion { get; set; }
        public string AndroidVersionName { get; set; }
        public string IosVersion { get; set; }
        public string IosVersionName { get; set; }
        public string UpdateInfo { get; set; }
        public string ApkUrl { get; set; }
        public string IpaUrl { get; set; }
        public string AndroidUpdateUrl { get; set; }
        public string IosUpdateUrl { get; set; }
        public UpdaeTypes UpdaeType { get; set; }
        public DateTime CreateTime { get; set; }

        public PassAppStore PassAppStore { get; set; }
    }
}
