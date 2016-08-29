using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
    public enum InteractiveTypes
    {
        [Description("全部")]
        None = 0,
        [Description("手机")]
        Phone = 1,
        [Description("邮箱")]
        Mail = 2
    }
}
