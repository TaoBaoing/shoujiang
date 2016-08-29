using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasicCommon;

namespace BasicData
{
    public class PermissionResourceValidator : AbstractValidator<PermissionResource>
    {
        public PermissionResourceValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("名称不能为空").NotEmpty().WithMessage("名称不能为空").Length(2, 10).WithMessage("名称长度必须2-10!");
            RuleFor(x => x.Comment).Length(0, 200).WithMessage("备注不能超过200字!");
            RuleFor(x => x.Depth).LessThanOrEqualTo(UPMSConfig.CountDepth).WithMessage(string.Format("允许的最大深度为{0}", UPMSConfig.CountDepth));

            Custom(i => (string.IsNullOrWhiteSpace(i.ControllerName) == string.IsNullOrWhiteSpace(i.ActionName)) ? null : new ValidationFailure("", "控制器和方法必须都为空或都非空"));

            Custom(i =>
            {
                if (i.Depth <= UPMSConfig.MenuDepth)
                {
                    //if (string.IsNullOrWhiteSpace(i.LinkUrl))
                    //{
                    //    return new ValidationFailure("", "菜单地址不能为空!");
                    //}
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(i.LinkUrl) != string.IsNullOrWhiteSpace(i.ElementSelector))
                    {
                        return new ValidationFailure("", "页面地址和页面元素必须都为空或都非空");
                    }
                    if (string.IsNullOrWhiteSpace(i.ElementSelector) && string.IsNullOrWhiteSpace(i.ControllerName))
                    {
                        return new ValidationFailure("", "权限资源无效");
                    }
                }
                return null;
            });
        }
    }
}