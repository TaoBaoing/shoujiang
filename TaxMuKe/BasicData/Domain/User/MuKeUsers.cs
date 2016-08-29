using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicData
{
    [Validator(typeof(UsersValidator))]
    public partial class MuKeUsers
    {
        public MuKeUsers()
        {
            this.MetaData = new List<UserMeta>();
        }

        public long Id { get; set; }
        public string uuid { get; set; }//环信i
        public string eid { get; set; }//环信id
        public string epwd { get; set; }//环信密码
        public string LoginName { get; set; }//昵称
        public string Name { get; set; } //真实姓名
        public string Interest { get; set; } //兴趣方向
        public string FinancialScale { get; set; } //财务规模
        public bool Sex { get; set; }//性别


        public string Password { get; set; }
        public MuKeUserTypes UserType { get; set; }

        [NotMapped]
        public DateTime? FirstPayDateTime { get; set; }
        public DateTime? DueDateTime { get; set; }//会员到期日期 
        public long RoleId { get; set; }

        public bool IsTeacher { get; set; }//是否老师
        public bool IsAdmin { get; set; }//是否管理员
        public string Email { get; set; }//邮箱
        public string Phone { get; set; }

        public string LinkContact { get; set; }

        public string AvatarUri { get; set; }//头像
        public string Job { get; set; }//职务
        public string Company { get; set; }//公司
        public MuKeEnabledStatus Status { get; set; }
        public string Description { get; set; }
        public string LastConnectIp { get; set; }
        public DateTime? LastConnectTime { get; set; }
        public long OnlineStatus { get; set; }

        public string Token { get; set; }
        public long CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
        public long UpdateUser { get; set; }
        public DateTime? UpdateTime { get; set; }
        public virtual PermissionRole Role { get; set; }

        public virtual ICollection<UserMeta> MetaData { get; set; }
        public virtual ICollection<UserHistory> Historys { get; set; }


    }
}
