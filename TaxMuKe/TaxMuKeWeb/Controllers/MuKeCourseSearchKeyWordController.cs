using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicData.Domain.Course;
using BasicFramework.Component;
using BasicUPMS.Models;
using BasicUPMS.Models.ViewModels.Requester;

namespace BasicUPMS.Controllers
{
    public class MuKeCourseSearchKeyWordController:CURDTemplate<MuKeCourseSearchKeyWord>
    {

        public ActionResult Index(MuKeCourseSearchKeyWordRequest request)
        {
            IQueryable<MuKeCourseSearchKeyWord> data = SessionContext.Repository.MuKeCourseSearchKeyWords;
            if (!string.IsNullOrEmpty(request.Name))
            {
                data = data.Where(x => x.KeyWord.Contains(request.Name));
            }

            var list = data.OrderByDescending(x => x.SearchCount+x.ManualCount).ToPagedList(request.PageIndex, request.PageSize);

            ViewBag.PageIndex = request.PageIndex;
            ViewBag.PageSize = request.PageSize;
            if (Request.IsAjaxRequest())
                return PartialView("_Items", list);
            return View(list);

        }
    }
}