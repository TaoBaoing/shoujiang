using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicData;
using BasicFramework.Component;
using BasicUPMS.Models;
using ThirdApi.qiniu.api;

namespace BasicUPMS.Controllers
{
    public class MuKeAppStoreSetController:CURDTemplate<AppStoreSet>
    {
        public ActionResult Index()
        {
            IQueryable<AppStoreSet> query = SessionContext.Repository.AppStoreSets;

            
           
            // 数据分页数据
            var data = query.OrderByDescending(x => x.Id).ToPagedList(0, 100);


            if (Request.IsAjaxRequest())
                return PartialView("_Items", data);
            return View(data);
        }
    }
}