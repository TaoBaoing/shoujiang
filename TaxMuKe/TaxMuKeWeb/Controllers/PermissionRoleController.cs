using BasicUPMS.Models;
using BasicUPMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using BasicFramework.Component;
using BasicUPMS.Resources;
using BasicData;
using BasicCommon;

namespace BasicUPMS.Controllers
{
    public class PermissionRoleController : CURDTemplate<PermissionRole>
    {

        public ActionResult Index(SearchRoleRequester request)
        {
            var expression = Fast.True<PermissionRole>();
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                expression = expression.And(i => i.Name.Contains(request.Name));
            }
            if (request.Status != MuKeEnabledStatus.None)
            {
                expression = expression.And(i => i.Status == request.Status);
            }
            var data = SessionContext.Repository.QueryableRoles.Where(expression).OrderByDescending(i => i.Sort).OrderByDescending(i => i.Id).ToPagedList(request.PageIndex, request.PageSize);
            if (Request.IsAjaxRequest())
                return PartialView("_Items", data);

            return View(data);
        }

        /// <summary>
        /// 加载权限列表，并选中指定角色拥有的权限(本方法作为UPMS的核心方法，故特意提供json与view两种方式返回，灵活应对各种视图)
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="style">0:empty view, 1: json or 2:has data view</param>
        /// <returns></returns>
        public ActionResult PermissionSetting(long roleId, int style = 0)
        {
            ViewBag.RoleId = roleId;
            if (style == 0)
                return View();
            else
            {
                PermissionService permissionService = new PermissionService();
                var selectedNodes = SessionContext.Repository.Permissions.Where(i => i.RoleId == roleId).Select(i => (long)i.PermissionResourceId).ToList();
                var vm = permissionService.GetPermissionResourceTree(SessionContext.Principal.RoleId, 0, UPMSConfig.CountDepth, 0, selectedNodes, PermissionTypes.None, MuKeEnabledStatus.Enabled);

                if (style == 1)
                    return Json(vm, JsonRequestBehavior.AllowGet);
                else if (style == 2)
                    return View(vm);
                else
                    return HttpNotFound();
            }
        }

        [HttpPost]

        public JsonResult PermissionSetting(long roleId, string resourceIds)
        {
            //校验
            long tmpVali;
            if (roleId <= 0 || !resourceIds.Split(',').Any(i => long.TryParse(i, out tmpVali) && tmpVali > 0))
            {
                return Json(new ResultModel(false, -1, ErrorMessage.ArgumentError));
            }

            //先删除
            SessionContext.Repository.Permissions.RemoveRange(SessionContext.Repository.Permissions.Where(i => i.RoleId == roleId));
            //添加
            var items = resourceIds.Split(',').Select(i => new Permissions { AuthType = AuthTypes.Allow, PermissionResourceId = int.Parse(i), RoleId = roleId }).ToList();
            SessionContext.Repository.Permissions.AddRange(items);

            return Json(SaveChanges());
        }

        [HttpPost]
        public override JsonResult Remove(long id)
        {
            ResultModel result;
            if (id <= 0)
            {
                result = new ResultModel(false) { Code = -1, Message = ErrorMessage.ArgumentError };
            }
            var entity = SessionContext.Repository.Roles.Find(id);
            if (entity == null)
            {
                result = new ResultModel(false) { Code = -1, Message = ErrorMessage.ArgumentError };
            }
            else if (SessionContext.Repository.MuKeUsers.Any(i => i.RoleId == id))
            {
                result = new ResultModel(false) { Code = -1, Message = "该角色下有用户，不能删除" };
            }
            else
            {
                SessionContext.Repository.Roles.Remove(entity);
                result = SaveChanges();
            }
            return Json(result);
        }
    }
}
