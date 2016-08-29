using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicData
{
    public class PermissionsValidator : AbstractValidator<Permissions>
    {
        public PermissionsValidator()
        {
            RuleFor(x => x.RoleId).GreaterThan(0).WithMessage("角色不能为空!");
            RuleFor(x => x.PermissionResourceId).GreaterThan(0).WithMessage("权限资源不能为空!");
        }
    }
}