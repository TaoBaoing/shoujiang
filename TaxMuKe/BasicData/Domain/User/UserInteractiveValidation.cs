using System;
using System.Collections.Generic;

namespace BasicData
{
    /// <summary>
    /// �û�������֤    �ж��û��������֤���Ƿ���Ч
    /// </summary>
    public partial class MuKeUserInteractiveValidation
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public InteractiveTypes InteractiveType { get; set; }
        public string RequestContent { get; set; }//�ֻ���
        public string ValidateContent { get; set; }//��֤��
        public DateTime ExpiredTime { get; set; }
        public MuKeEnabledStatus Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
