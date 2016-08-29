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
        public string uuid { get; set; }//����i
        public string eid { get; set; }//����id
        public string epwd { get; set; }//��������
        public string LoginName { get; set; }//�ǳ�
        public string Name { get; set; } //��ʵ����
        public string Interest { get; set; } //��Ȥ����
        public string FinancialScale { get; set; } //�����ģ
        public bool Sex { get; set; }//�Ա�


        public string Password { get; set; }
        public MuKeUserTypes UserType { get; set; }

        [NotMapped]
        public DateTime? FirstPayDateTime { get; set; }
        public DateTime? DueDateTime { get; set; }//��Ա�������� 
        public long RoleId { get; set; }

        public bool IsTeacher { get; set; }//�Ƿ���ʦ
        public bool IsAdmin { get; set; }//�Ƿ����Ա
        public string Email { get; set; }//����
        public string Phone { get; set; }

        public string LinkContact { get; set; }

        public string AvatarUri { get; set; }//ͷ��
        public string Job { get; set; }//ְ��
        public string Company { get; set; }//��˾
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
