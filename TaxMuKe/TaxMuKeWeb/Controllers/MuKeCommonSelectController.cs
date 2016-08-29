using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicData;
using BasicFramework.Component;
using BasicUPMS.Models;
using BasicUPMS.Models.ViewModels.Requester;

namespace BasicUPMS.Controllers
{
    public class MuKeCommonSelectController : BaseController
    {
        public ActionResult MuKeUser(string viewName, MuKeCommonUserSelectRequester requester)
        {
            IQueryable<MuKeUsers> queryable=SessionContext.Repository.MuKeUsers.Where(x=>x.Status==MuKeEnabledStatus.Enabled);
            if (!string.IsNullOrWhiteSpace(requester.UserName))
            {
                queryable = queryable.Where(item => item.LoginName.Contains(requester.UserName));
            }
            if (!string.IsNullOrWhiteSpace(requester.Phone))
            {
                queryable = queryable.Where(item => item.Phone==requester.Phone);
            }
            var data = queryable.OrderByDescending(x=>x.Id).ToPagedList(requester.PageIndex, requester.PageSize);

            return View(viewName, data);
        }
        public ActionResult Course(string viewName, MuKeCommonSelectRequester requester)
        {
            IQueryable<MuKeCourse> queryable=SessionContext.Repository.MuKeCourses.Where(x=>x.Status==MuKeEnabledStatus.Enabled&&x.IsDelete==false);
            if (!string.IsNullOrWhiteSpace(requester.Title))
            {
                queryable = queryable.Where(item => item.Title.Contains(requester.Title));
            }
            var data = queryable.OrderByDescending(x=>x.Id).ToPagedList(requester.PageIndex, requester.PageSize);

            return View(viewName, data);
        }
      
    }
}