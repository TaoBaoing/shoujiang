using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Domain
{
   public  class MuKeGlobalSet
    {
       public long Id { get; set; }

       public GlobalSetType GlobalSetType { get; set; }

       public decimal DecimalValue { get; set; }

       public string Title { get; set; }
       public string Content { get; set; }
    
       public DateTime CreateTime { get; set; }
    }

    public enum GlobalSetType
    {

        [Description("关于我们")]
        AboutUs =1,

        [Description("服务协议")]
        ServiceAgreement =2,

        [Description("E金会员设置")]
        GoldUser = 3,

        [Description("E尊会员设置")]
        WhiteGoldUser = 4,

        [Description("E金升E尊会员设置")]
        GlodUpgradeWhiteGoldUser = 5,

        [Description("修改admin 密码 ")]
        ChangeAdminPassword = 6,
    }
}
