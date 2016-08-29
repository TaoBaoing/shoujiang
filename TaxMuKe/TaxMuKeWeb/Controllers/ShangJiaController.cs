using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicData.Domain;
using BasicFramework.Component;
using BasicUPMS.Models;

namespace BasicUPMS.Controllers
{
    public class ShangJiaController:CURDTemplate<ShangJia>
    {
        public ActionResult Index()
        {
            IQueryable<ShangJia> data = SessionContext.Repository.ShangJias;
            var list = data.OrderByDescending(x => x.Id).ToPagedList(1, 10);

            ViewBag.PageIndex = 1;
            ViewBag.PageSize = 10;
            if (Request.IsAjaxRequest())
                return PartialView("_Items", list);
            return View(list);
        }
    }
}