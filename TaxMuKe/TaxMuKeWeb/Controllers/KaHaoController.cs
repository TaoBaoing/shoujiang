using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            IQueryable<KaHao> data = SessionContext.Repository.KaHaos.Include(x=>x.MuKeDirtyWord);
            if (SessionContext.Principal.RoleId > 0)
            {
                data = data.Where(x => x.MuKeDirtyWord.Id == SessionContext.Principal.RoleId);
            }

            var list = data.OrderByDescending(x => x.Id).ToPagedList(request.PageIndex, request.PageSize);

            ViewBag.PageIndex = request.PageIndex;
            ViewBag.PageSize = request.PageSize;
            ViewBag.RoleId = SessionContext.Principal.RoleId;
            if (Request.IsAjaxRequest())
                return PartialView("_Items", list);
            return View(list);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override JsonResult Add(KaHao requet)
        {
            requet.MuKeDirtyWordId = SessionContext.Principal.RoleId ;
            return base.Add(requet);
        }
    }
}