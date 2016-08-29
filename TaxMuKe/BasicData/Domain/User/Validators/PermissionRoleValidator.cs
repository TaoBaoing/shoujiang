using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicData
{
    public class PermissionRoleValidator : AbstractValidator<PermissionRole>
    {
        public PermissionRoleValidator()
        {
            RuleFor(x => x.Name).Length(2, 10).WithMessage("名称长度必须2-10!").NotNull().WithMessage("名称不能为空").NotEmpty().WithMessage("名称不能为空");
            RuleFor(x => x.Comment).Length(0, 200).WithMessage("备注不能超过200字!");
        }
    }
}