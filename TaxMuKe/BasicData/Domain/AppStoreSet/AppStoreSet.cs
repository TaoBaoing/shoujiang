using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
    public  class AppStoreSet
    {
        public long Id { get; set; }

        public string VersionName  { get; set; }

        public PassedAppStore PassedAppStore { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public enum PassedAppStore
    {
        [Description("全部")]
        None = 0,

        [Description("未通过")]
        NotPass = 1,

        [Description("已通过")]
        Passed = 2,
    }
}
