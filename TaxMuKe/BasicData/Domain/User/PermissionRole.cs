using FluentValidation.Attributes;
using System;
using System.Collections.Generic;

namespace BasicData
{
    [Validator(typeof(PermissionRoleValidator))]
    public partial class PermissionRole
    {
        public PermissionRole()
        {
            this.Permissions = new List<Permissions>();
            this.Users = new List<MuKeUsers>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public MuKeEnabledStatus Status { get; set; }
        public int Sort { get; set; }
        public string Comment { get; set; }
        public DateTime CreateTime { get; set; }
        public virtual ICollection<Permissions> Permissions { get; set; }
        public virtual ICollection<MuKeUsers> Users { get; set; }
    }
}
