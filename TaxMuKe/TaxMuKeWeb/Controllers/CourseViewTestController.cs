using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicData.Domain.Course;
using BasicFramework.Component;
using BasicUPMS.Models;

namespace BasicUPMS.Controllers
{
    public class CourseViewTestController:BaseController
    {
        public ActionResult Index(SearchContentsRequester request)
        {
            
            IQueryable<MuKeCourseTestRecord> queryable = SessionContext.Repository.MuKeCourseTestRecords.Include(x => x.User).Include(x => x.MuKeCourse).Where(x => x.MuKeCourseId == request.CourseId);

            if (!string.IsNullOrEmpty(request.UserName))
            {
                queryable= SessionContext.Repository.MuKeCourseTestRecords.Include(x => x.User).Include(x => x.MuKeCourse).Where(x => x.MuKeCourseId == request.CourseId).Where(x=>x.User.LoginName.Contains(request.UserName));
            }

            var data = queryable
            .OrderByDescending(i => i.Id)
            .ToPagedList(request.PageIndex, request.PageSize);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Items", data);
            }
            ViewBag.CourseId = request.CourseId;
            return View(data);
        }
    }
}