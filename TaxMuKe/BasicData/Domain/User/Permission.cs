using FluentValidation.Attributes;
using System;
using System.Collections.Generic;

namespace BasicData
{
    [Validator(typeof(PermissionsValidator))]
    public partial class Permissions
    {
        public long Id { get; set; }
        public long PermissionResourceId { get; set; }
        public long RoleId { get; set; }
        public AuthTypes AuthType { get; set; }
        public DateTime CreateTime { get; set; }
        public virtual PermissionResource PermissionResource { get; set; }
        public virtual PermissionRole Role { get; set; }
    }
}
