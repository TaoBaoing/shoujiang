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
    public class PermissionResourceController : CURDTemplate<PermissionResource>
    {

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetTreeData(long currentNodeId)
        {
            PermissionService permissionService = new PermissionService();
            var vm = permissionService.GetPermissionResourceTree(SessionContext.Principal.RoleId, 0, UPMSConfig.CountDepth, currentNodeId, null, PermissionTypes.None, MuKeEnabledStatus.None);
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override JsonResult Add(PermissionResource requet)
        {
            ReslovePermissionType(requet);
            return base.Add(requet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override JsonResult Update([ModelBinder(typeof(UpdateableModelBinder))]PermissionResource requet)
        {
            ReslovePermissionType(requet);
            return Json(CheckModelCallback<long>(() => { return requet.ParentId; }));
        }

        [NonAction]
        private void ReslovePermissionType(PermissionResource requet)
        {

            PermissionTypes permissionType = PermissionTypes.None;
            //功能权限
            if (!(string.IsNullOrWhiteSpace(requet.ControllerName) || string.IsNullOrWhiteSpace(requet.ActionName)))
            {
                permissionType = PermissionTypes.Feature;
            }
            if (requet.Depth <= UPMSConfig.MenuDepth)
            {
                //菜单权限
                if (permissionType == PermissionTypes.None)
                {
                    permissionType = PermissionTypes.Menu;
                }
                else
                {
                    permissionType = permissionType | PermissionTypes.Menu;
                }
            }
            else
            {
                //页面元素权限
                if (!string.IsNullOrWhiteSpace(requet.ElementSelector))
                {
                    if (permissionType == PermissionTypes.None)
                    {
                        permissionType = PermissionTypes.PageElements;
                    }
                    else
                    {
                        permissionType = permissionType | PermissionTypes.PageElements;
                    }
                }
            }
            requet.PermissionType = permissionType;
        }
    }
}
