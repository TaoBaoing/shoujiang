using System;
using System.Collections.Generic;

namespace BasicData
{
    /// <summary>
    /// 用户交互验证    判断用户输入的验证码是否有效
    /// </summary>
    public partial class MuKeUserInteractiveValidation
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public InteractiveTypes InteractiveType { get; set; }
        public string RequestContent { get; set; }//手机号
        public string ValidateContent { get; set; }//验证码
        public DateTime ExpiredTime { get; set; }
        public MuKeEnabledStatus Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
