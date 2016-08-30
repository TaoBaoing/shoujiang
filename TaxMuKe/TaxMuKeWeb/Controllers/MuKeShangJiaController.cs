using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicData.Domain;
using BasicFramework.Component;
using BasicUPMS.Models;
using BasicUPMS.Models.ViewModels.Requester;

namespace BasicUPMS.Controllers
{
    public class MuKeShangJiaController : CURDTemplate<MuKeShangJia>
    {
        public ActionResult Index(MuKeDirdyWordRequest request)
        {
            IQueryable<MuKeShangJia> data = SessionContext.Repository.MuKeShangJias;
            if (!string.IsNullOrEmpty(request.Name))
            {
                data = data.Where(x => x.Name.Contains(request.Name));
            }

            var list = data.OrderByDescending(x => x.Id).ToPagedList(request.PageIndex, request.PageSize);

            ViewBag.PageIndex = request.PageIndex;
            ViewBag.PageSize = request.PageSize;
            if (Request.IsAjaxRequest())
                return PartialView("_Items", list);
            return View(list);

        }
    }
}