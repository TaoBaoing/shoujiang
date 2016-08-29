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
                IssuedPassport(name, UPMSConfig.SystemRole + "|" + 888888);
                return RedirectToAction("Index");
            }
            else
            {
                password = Utilities.MD5(password);
                var userData = SessionContext.Repository.MuKeUsers.SingleOrDefault(i => i.IsAdmin == true && i.LoginName == name && i.Password == password && i.Status == MuKeEnabledStatus.Enabled);
                if (userData != null)
                {
                    //颁发通行证
                    IssuedPassport(name, userData.RoleId + "|" + userData.Id);

                    #region  处理页面元素权限
                    //>>>>>>>>>>>>>>>>将黑名单写入cookie<<<<<<<<<<<<<<<<<<<<<<<<<<<

                    ////黑名单
                    //var blacklist = (from a in SessionContext.Repository.PermissionResources.Where(i =>
                    //    i.Status == EnabledStatus.Enabled &&
                    //    (i.PermissionType & PermissionTypes.PageElements) == PermissionTypes.PageElements &&
                    //    !(string.IsNullOrEmpty(i.LinkUrl) || string.IsNullOrEmpty(i.ElementSelector))
                    //    )
                    //                 join b in SessionContext.Repository.Permissions.Where(i => i.RoleId == userData.RoleId)
                    //                 on a.Id equals b.PermissionResourceId into c
                    //                 from d in c.DefaultIfEmpty()
                    //                 where (d.Id == null)
                    //                 select new { k = a.LinkUrl, v = a.ElementSelector }).ToList();

                    //if (blacklist != null && blacklist.Count > 0)
                    //{
                    //    //整理黑名单
                    //    Dictionary<string, string> blacklistDictionary = new Dictionary<string, string>();
                    //    blacklist.ForEach((i) =>
                    //    {
                    //        if (blacklistDictionary.ContainsKey(i.k))
                    //            blacklistDictionary[i.k] += "," + i.v;
                    //        else
                    //            blacklistDictionary.Add(i.k, i.v);
                    //    });
                    //    string blacklistString = string.Empty;
                    //    blacklistDictionary.ForEach((i) => { blacklistString += "'" + i.Key + "~" + i.Value; });

                    //    //Expires与票据cookie相同
                    //    HttpCookie blacklistCookie = new HttpCookie("blacklist", blacklistString) { Expires = DateTime.Now.Add(FormsAuthentication.Timeout) };
                    //    Response.Cookies.Add(blacklistCookie);
                    //}
                    #endregion

                    #region 更新登陆信息
                    userData.LastConnectIp = Utilities.GetWebClientIp();
                    userData.LastConnectTime = DateTime.Now;
                    SessionContext.Repository.SaveChanges();
                    #endregion

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
        private void IssuedPassport(string name, string userData)
        {
            //票据
            var timeout = DateTime.Now.Add(FormsAuthentication.Timeout);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, name, DateTime.Now,
                timeout, false, userData);
            //加密票据
            var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            //写入cookie
            HttpCookie ticketCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket) { HttpOnly = true, Expires = timeout };
            Response.Cookies.Add(ticketCookie);
        }

    }
}
