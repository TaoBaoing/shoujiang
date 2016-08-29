using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicData.Domain.Course;
using BasicFramework.Component;
using BasicUPMS.Models;
using BasicUPMS.Models.ViewModels.Requester;

namespace BasicUPMS.Controllers
{
   
    public class MuKeCourseCommentController: BaseController
    {
        public ActionResult Index(SearchCourseCommentRequester request)
        {

            IQueryable<MuKeCourseCapterComment> queryable = null;
            if (request.CourseId <= 0)
            {
                queryable = SessionContext.Repository.MuKeCourseCapterComments.Include(x => x.User);
            }
            else
            {
                queryable = SessionContext.Repository.MuKeCourseCapterComments.Include(x => x.User).Where(x=>x.CourseId==request.CourseId);
            }
          
            var data = queryable.OrderByDescending(x => x.CreateTime).ToPagedList(request.PageIndex, request.PageSize);

            ViewBag.CourseId = request.CourseId;
            return View(data);
        }
    }
}