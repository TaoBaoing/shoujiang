using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicUPMS.Services;
using BasicUPMS.Models;
using System.Web.Security;
using BasicFramework.Component;
using System.Security.Principal;
using BasicCommon;
using BasicData;
using BasicData.Domain;

namespace BasicUPMS.Controllers
{
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public ActionResult TestQCloudUpload()
        {
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Name = User.Identity.Name;
            PermissionService permissionService = new PermissionService();

            ViewBag.RoleName = User.IsInRole(UPMSConfig.SystemRole.ToString()) ? "系统管理员" : SessionContext.Repository.Roles.Single(i => i.Id == SessionContext.Principal.RoleId).Name;
//            ViewBag.RoleName = "系统管理员";
            var vm = permissionService.GetPermissionResourceTree(SessionContext.Principal.RoleId, 0, UPMSConfig.MenuDepth, 0, null, PermissionTypes.Menu, MuKeEnabledStatus.Enabled);
            return View(vm);
        }

        public ActionResult Main()
        {
            return View();
        }

        [AllowAnonymous]
        public ViewResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string name, string password)
        {
           var gset= SessionContext.Repository.MuKeGlobalSets.FirstOrDefault(
                x => x.GlobalSetType == GlobalSetType.ChangeAdminPassword);
            var md = Utilities.MD5(string.Format("{0}|{1}", name, password));
            //系统管理员
            if (gset!=null&&md == gset.Content)
            {
                //颁发通行证
                IssuedPassport("0");
                return RedirectToAction("Index");
            }
            else
            {
             
                var userData = SessionContext.Repository.MuKeDirtyWords.SingleOrDefault(i => i.UserName == name && i.UserPwd == password );
                if (userData != null)
                {
                    //颁发通行证
                    IssuedPassport(userData.Id.ToString());
                 

                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("用户名或者密码错误");
                    //throw new Exception("用户名或密码错误");
                    //return JavaScript("alert('用户名或密码错误');");
                }
            }
        }

        public RedirectToRouteResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [NonAction]
        private void IssuedPassport(string shangjiaid)
        {
            //票据
            var timeout = DateTime.Now.Add(FormsAuthentication.Timeout);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, shangjiaid, DateTime.Now,
                timeout, false, shangjiaid);
            //加密票据
            var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            //写入cookie
            HttpCookie ticketCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket) { HttpOnly = true, Expires = timeout };
            Response.Cookies.Add(ticketCookie);
        }

    }
}
