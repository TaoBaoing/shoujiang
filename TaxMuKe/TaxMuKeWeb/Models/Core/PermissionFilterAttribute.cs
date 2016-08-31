using BasicFramework.Component;
using BasicUPMS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using BasicData;
using BasicCommon;

namespace BasicUPMS.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class PermissionFilterAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var actionName = filterContext.ActionDescriptor.ActionName;
            if (controllerName == "KaHaoRecord" && actionName == "GetCanUseLucky")
            {
                return;
            }
            //            SessionContext.Logger.Debug("controllerName:"+ controllerName+ "    actionName:"+ actionName);
                if (controllerName == "Material" && actionName == "QCloudTransCodeCallBack")
            {
                //如果是腾讯视频转码回调，则跳过
                return;
            }
            //校验Referrer信息
            if (UPMSConfig.CkeckUrlReferrer)
            {
                if (!((controllerName == "Home" && (actionName == "Login" || actionName == "Index"))) &&
                    (filterContext.HttpContext.Request.UrlReferrer == null
                    ||
                    filterContext.HttpContext.Request.UrlReferrer.Host != filterContext.HttpContext.Request.Url.Host
                    ))
                {
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                    return;
                }
            }

            //排除Home控制器
            if (controllerName == "Home")
            {
                return;
            }
            //系统管理员，放开所有限制
            if (SessionContext.Principal.IsInRole(UPMSConfig.SystemRole.ToString()))
            {
                return;
            }
            //非系统管理员，不允许访问或修改权限资源
            else if (controllerName == "PermissionResource")
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                return;
            }
            var permissionResources = SessionContext.Repository.PermissionResources.SingleOrDefault(i =>
                (i.PermissionType & PermissionTypes.Feature) == PermissionTypes.Feature &&
                i.ControllerName == controllerName &&
                i.ActionName == actionName &&
                i.Status == MuKeEnabledStatus.Enabled
                );
            //////不在权限检查范围之内
            ////if (permissionResources == null)
            ////{
            ////    return;
            ////}
            //////检查权限
            ////var isAllow = SessionContext.Repository.Permissions.Count(i =>
            ////      i.PermissionResourceId == permissionResources.Id &&
            ////      i.RoleId == SessionContext.Principal.RoleId &&
            ////      i.AuthType == AuthTypes.Allow
            ////      ) > 0;
            ////if (isAllow)
            ////{
            ////    return;
            ////}
            ////else
            ////{
            ////    //如果是Ajax请求，返回json格式错误信息
            ////    if (filterContext.HttpContext.Request.IsAjaxRequest())
            ////    {
            ////        filterContext.Result = new JsonResult() { Data = new ResultModel(false, -1, ErrorMessage.AuthenticationFailed) };
            ////    }
            ////    //如果是普通页面，返回未授权
            ////    else
            ////    {
            ////        filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            ////    }
            ////}
        }
    }
}
