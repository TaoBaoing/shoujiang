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
    public class KaHaoController : CURDTemplate<KaHao>
    {
        public ActionResult Index(MuKeDirdyWordRequest request)
        {
            IQueryable<KaHao> data = SessionContext.Repository.KaHaos;
            

            var list = data.OrderByDescending(x => x.Id).ToPagedList(request.PageIndex, request.PageSize);

            ViewBag.PageIndex = request.PageIndex;
            ViewBag.PageSize = request.PageSize;
            if (Request.IsAjaxRequest())
                return PartialView("_Items", list);
            return View(list);

        }
    }
}