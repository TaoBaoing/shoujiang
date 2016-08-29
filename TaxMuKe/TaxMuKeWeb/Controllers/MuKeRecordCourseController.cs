using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicData;
using BasicData.Domain.Course;
using BasicFramework.Component;
using BasicUPMS.Models;
using BasicUPMS.Models.ViewModels.Requester;

namespace BasicUPMS.Controllers
{
    public class MuKeRecordCourseController : BaseController
    {
        public ActionResult Index(MuKeRecordCourseRequest request)
        {
            IQueryable<MuKeRecordCourse> query = SessionContext.Repository.MuKeRecordCourses.Include(x=>x.Course);
            if (request.UserId > 0)
            {
                query = query.Where(x => x.UserId == request.UserId);
            }
            // 数据分页数据
            var data = query.OrderByDescending(x => x.CreateTime).ToPagedList(request.PageIndex, request.PageSize);

            if (Request.IsAjaxRequest())
                return PartialView("_Items", data);
            return View(data);
        }
    }
}