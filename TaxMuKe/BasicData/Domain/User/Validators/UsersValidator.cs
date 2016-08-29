using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace BasicData
{
    public class UsersValidator : AbstractValidator<MuKeUsers>
    {
        public UsersValidator()
        {
//            RuleFor(x => x.LoginName).NotNull().WithMessage("名称不能为空").Length(2, 20).WithMessage("登陆名称长度必须2-20!");
//            RuleFor(x => x.RoleId).GreaterThan(0).WithMessage("请选择角色!");
            RuleFor(x => x.Phone).Matches("^13[0-9]{9}$|14[0-9]{9}|17[0-9]{9}|15[0-9]{9}$|18[0-9]{9}$").WithMessage("手机号码格式不正确!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("邮箱格式不正确!");
            RuleFor(x => x.Description).Length(0, 200).WithMessage("备注不能超过200字!");

        }
    }
}