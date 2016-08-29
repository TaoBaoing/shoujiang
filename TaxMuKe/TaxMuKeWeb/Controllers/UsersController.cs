using BasicUPMS.Models;
using BasicUPMS.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using BasicFramework.Component;
using BasicUPMS.Resources;
using System.Web.Security;
using BasicData;
using BasicCommon;
using BasicData.Domain;
using System.ComponentModel;

namespace BasicUPMS.Controllers
{
    public class UsersController : CURDTemplate<MuKeUsers>
    {
        public ActionResult Detail()
        {
            ViewBag.IP = Utilities.GetWebClientIp();
            var vm = new MuKeUsers();
            if (!User.IsInRole(UPMSConfig.SystemRole.ToString()))
                vm = SessionContext.Repository.MuKeUsers.SingleOrDefault(i => i.LoginName == User.Identity.Name);
            return View(vm);
        }

        public ActionResult Index(SearchUserRequester request)
        {
            var expression = Fast.True<MuKeUsers>();
            if (!string.IsNullOrWhiteSpace(request.Phone))
            {
                expression = expression.And(i => i.Phone == request.Phone);
            }
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                expression = expression.And(i => i.Name.Contains(request.Name));
            }
            if (!string.IsNullOrWhiteSpace(request.LogicName))
            {
                expression = expression.And(i => i.LoginName.Contains(request.LogicName));
            }
            if (request.Status != MuKeEnabledStatus.None)
            {
                expression = expression.And(i => i.Status == request.Status);
            }
            if (request.RoleId > 0)
            {
                expression = expression.And(i => i.RoleId == request.RoleId);
            }
            if (request.UserType != MuKeUserTypes.None)
            {
                expression = expression.And(i => i.UserType == request.UserType);
            }
            if (request.txtStartTime.HasValue)
            {
                expression = expression.And(x => x.CreateTime >= request.txtStartTime.Value);
            }
            if (request.txtEndTime.HasValue)
            {
                var endTime = request.txtEndTime.Value.AddDays(1);
                expression = expression.And(x => x.CreateTime < endTime);
            }
            var data = SessionContext.Repository.QueryableUsers.Where(expression).OrderByDescending(i => i.Id).ToPagedList(request.PageIndex, request.PageSize);
            if (Request.IsAjaxRequest())
                return PartialView("_Items", data);
            //角色下拉菜单
            var roles = SessionContext.Repository.Roles.OrderByDescending(i => i.Id).ToList().Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).ToList();
            roles.Insert(0, new SelectListItem { Text = "全部", Value = "0" });
            ViewBag.Roles = roles;

            return View(data);
        }

        /// <summary>
        /// 拼dataset导出Excel
        /// </summary>
        /// <param name="name"></param>
        public ActionResult ExportExcel(string LogicName, string Phone, DateTime? txtStartTime, DateTime? txtEndTime, MuKeUserTypes UserType, MuKeEnabledStatus Status)
        {
            //接收需要导出的数据
            IQueryable<MuKeUsers> listsignup = SessionContext.Repository.MuKeUsers.OrderByDescending(x => x.Id);
            if (!string.IsNullOrWhiteSpace(LogicName))
            {
                listsignup = listsignup.Where(x => x.LoginName.Contains(LogicName));
            }

            if (!string.IsNullOrWhiteSpace(Phone))
            {
                listsignup = listsignup.Where(x => x.Phone.Contains(Phone));
            }

            if (txtStartTime.HasValue)
            {
                listsignup = listsignup.Where(x => x.CreateTime >= txtStartTime.Value);
            }
            if (txtEndTime.HasValue)
            {
                var endTime = txtEndTime.Value.AddDays(1);
                listsignup = listsignup.Where(x => x.CreateTime < endTime);
            }

            if (UserType != MuKeUserTypes.None)
            {
                listsignup = listsignup.Where(i => i.UserType == UserType);
            }

            if (Status != MuKeEnabledStatus.None)
            {
                listsignup = listsignup.Where(i => i.Status == Status);
            }

            var signUpList = listsignup.ToList();

            var ds = new DataSet();
            using (DataTable dt = new DataTable())
            {
                //创建列
                DataColumn dtc = new DataColumn("ID", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("手机", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("昵称", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("姓名", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("用户类型", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("兴趣方向", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("财务规模", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("状态", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("创建时间", typeof(string));
                dt.Columns.Add(dtc);

                foreach (MuKeUsers item in signUpList)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = item.Id.ToString();
                    dr["手机"] = item.Phone.ToString();
                    dr["昵称"] = item.LoginName;
                    dr["姓名"] = item.Name;
                    dr["用户类型"] = getEnumDescription(item.UserType);
                    dr["兴趣方向"] = item.Interest;
                    dr["财务规模"] = item.FinancialScale;
                    dr["状态"] = getEnumDescription(item.Status);
                    dr["创建时间"] = item.CreateTime.ToString();

                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }

            return ExcelFile(ds, "客户管理");
            //命名导出表格的StringBuilder变量
            //StringBuilder sHtml = new StringBuilder(string.Empty);
            ////打印表头
            //sHtml.Append("<table border=\"1\" width=\"100%\">");
            //sHtml.Append("<tr height=\"40\"><td colspan=\"9\" align=\"center\" style='font-size:24px'><b>用户订单" + "</b></td></tr>");
            ////打印列名
            //sHtml.Append("<tr height=\"20\" align=\"center\" ><td>ID</td><td>手机</td><td>昵称</td><td>姓名</td><td>用户类型</td>"
            //    + "<td>兴趣方向</td><td>财务规模</td><td>状态</td><td>创建时间</td></tr>");
            ////循环读取List集合 
            //foreach (var item in signUpList)
            //{
            //    sHtml.Append("<tr height=\"20\" align=\"left\"><td>" + item.Id.ToString()
            //      + "</td><td>'" + item.Phone.ToString() + "</td><td>" + item.LoginName + "</td><td>" + item.Name
            //      + "</td><td>" + getEnumDescription(item.UserType) + "</td><td>" + item.Interest + "</td><td>" + item.FinancialScale
            //      + "</td><td>" + getEnumDescription(item.Status) + "</td><td>" + item.CreateTime.ToString() + "</td></tr>");
            //}
            ////打印表尾
            //sHtml.Append("</table>");
            ////调用输出Excel表的方法
            //ExportToExcel("application/ms-excel", "用户管理.xls", sHtml.ToString());
        }

        public string getEnumDescription(Enum selected)
        {
            return ((DescriptionAttribute)selected.GetType().GetField(selected.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false).First()).Description;
        }
        //public void ExportToExcel(string FileType, string FileName, string ExcelContent)
        //{
        //    System.Web.HttpContext.Current.Response.Charset = "UTF-8";
        //    System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        //    System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8).ToString());
        //    System.Web.HttpContext.Current.Response.ContentType = FileType;
        //    System.IO.StringWriter tw = new System.IO.StringWriter();
        //    System.Web.HttpContext.Current.Response.Output.Write(ExcelContent.ToString());
        //    System.Web.HttpContext.Current.Response.Flush();
        //    System.Web.HttpContext.Current.Response.End();
        //}


        public override PartialViewResult FormForAdd(string viewName, MuKeUsers entity)
        {
            //角色下拉菜单
            var roles = SessionContext.Repository.Roles.Where(i => i.Status == MuKeEnabledStatus.Enabled).OrderByDescending(i => i.Id).ToList().Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString(), Selected = entity.RoleId == i.Id }).ToList();
            ViewBag.Roles = roles;
            ViewBag.EditAction = "/Users/Add" ;
           
            return base.FormForAdd(viewName, entity);

        }
        public override PartialViewResult FormForUpdate(string viewName, long id)
        {
            //角色下拉菜单
            var result = base.FormForUpdate(viewName, id);
            var entity = result.Model as MuKeUsers;
            var roles = SessionContext.Repository.Roles.Where(i => i.Status == MuKeEnabledStatus.Enabled).OrderByDescending(i => i.Id).ToList().Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString(), Selected = entity.RoleId == i.Id }).ToList();
            ViewBag.Roles = roles;
            ViewBag.EditAction = "/Users/Update/" + id; ;
            return result;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override JsonResult Add(MuKeUsers requet)
        {
            if (string.IsNullOrWhiteSpace(requet.Password))
                requet.Password = UPMSConfig.DefaulPassword;
            if (SessionContext.Repository.MuKeUsers.Any(i => i.Phone == requet.Phone))
                return Json(new ResultModel<int>(false) { Code = -1, Message = "该手机号已经被注册" });
            requet.Password = Utilities.MD5(requet.Password);
            requet.CreateTime = DateTime.Now;
            requet.CreateUser = long.Parse(((FormsIdentity)SessionContext.Principal.Identity).Label);
            requet.RoleId = 1;//系统暂时没有用到角色 所以默认1
            return base.Add(requet);
        }

        [HttpPost]
        public JsonResult ResetPassword(long id)
        {
            return Json(CheckParameterCallback(() =>
            {
                var user = SessionContext.Repository.MuKeUsers.Find(id);
                user.Password = Utilities.MD5(UPMSConfig.DefaulPassword);
            }, id));
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string oldPassword, string newPassword)
        {
            if (!User.IsInRole(UPMSConfig.SystemRole.ToString()))
            {
                ResultModel result = new ResultModel(false);
                if (string.IsNullOrWhiteSpace(oldPassword))
                {
                    result.Message = "旧密码不能为空";
                }
                else if (string.IsNullOrWhiteSpace(newPassword))
                {
                    result.Message = "新密码不能为空";
                }
                else if (oldPassword == newPassword)
                {
                    result.Message = "新旧密码不能一样";
                }
                else
                {
                    var userEntity = SessionContext.Repository.MuKeUsers.SingleOrDefault(i => i.Name == User.Identity.Name);
                    if (userEntity.Password != Utilities.MD5(oldPassword))
                    {
                        result.Message = "旧密码不正确";
                    }
                    else
                    {
                        userEntity.Password = Utilities.MD5(newPassword);
                        result = SaveChanges();
                    }
                }
                if (result.IsSuccess)
                {
                    return RedirectToAction("Detail");
                }
                ModelState.AddModelError("", result.Message);

            }
            return View();
        }


        public ActionResult ChangePasswordForAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePasswordForAdmin(string oldPassword, string newPassword)
        {
            var userEntity = SessionContext.Repository.MuKeGlobalSets.FirstOrDefault(x => x.GlobalSetType == GlobalSetType.ChangeAdminPassword);
            if (userEntity == null)
            {
                return Content("未设置初始密码");
            }
            var md = Utilities.MD5(string.Format("{0}|{1}", "admin", oldPassword));
            if (userEntity.Content == md)
            {
                SessionContext.Repository.MuKeGlobalSets.Attach(userEntity);
                var newps = Utilities.MD5(string.Format("{0}|{1}", "admin", newPassword));
                userEntity.Content = newps;
                SessionContext.Repository.SaveChanges();
                return Content("修改成功");
            }
            else
            {
                return Content("旧密码不正确");
            }

        }

        public ActionResult ChangePasswordForUser(long userId)
        {
            var userItem = SessionContext.Repository.MuKeUsers.Find(userId);
            return View(userItem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePasswordForUser(long userId, string newPassword)
        {

            ResultModel result = new ResultModel(false);

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                result.Message = "新密码不能为空";
            }
            else
            {
                var userEntity = SessionContext.Repository.MuKeUsers.Find(userId);
                userEntity.Password = Utilities.MD5(newPassword);
                result = SaveChanges();

            }
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);

            return View();
        }
        
    }
}
