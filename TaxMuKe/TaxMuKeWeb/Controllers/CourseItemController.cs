using BasicUPMS.Models;
using BasicUPMS.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using BasicFramework.Component;
using BasicUPMS.Resources;
using System.Web.Security;
using BasicData;
using BasicCommon;
using System.IO;
using System.Web.Routing;
using BasicData.Domain.Course;


namespace BasicUPMS.Controllers
{
    public class CourseItemController : CURDTemplate<MuKeCourseCapter>
    {
        public ActionResult CreateExcel(long courseid)
        {
            var course = SessionContext.Repository.MuKeCourses.Find(courseid);

            var record =SessionContext.Repository.MuKeRecordCourseCapters.Include(x => x.User).Include(x=>x.CourseCapter)
                    .Where(x => x.CourseId == courseid).OrderByDescending(x=>x.CourseCapterId).ThenBy(x=>x.User.LoginName);

            var ds=new DataSet();
            using (DataTable dt = new DataTable())
            {
                //创建列
                DataColumn dtc = new DataColumn("章节", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("时长", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("点击数", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("用户", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("观看时长", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("观看完毕", typeof(string));
                dt.Columns.Add(dtc);

                foreach (MuKeRecordCourseCapter capter in record)
                {
                    DataRow dr = dt.NewRow();
                    dr["章节"] = capter.CourseCapter.Title;
                    dr["时长"] = DateTimeUtil.GetDateStrBySecond(capter.CourseCapter.TimeLength);
                    dr["点击数"] = capter.CourseCapter.ClickCount;
                    dr["用户"] = capter.User.LoginName;
                    dr["观看时长"] = DateTimeUtil.GetDateStrBySecond(capter.CurrentProcess);
                    if (capter.ViewComplete)
                    {
                        dr["观看完毕"] ="是";
                    }
                    else
                    {
                        dr["观看完毕"] = "否";
                    }
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }
            
            return ExcelFile(ds, course.Title);

        }

        public ActionResult Index(SearchContentsRequester request)
        {
            var expression = Fast.True<MuKeCourseCapter>();
            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                expression = expression.And(i => i.Title.Contains(request.Title));
            }
            if (request.Status != MuKeEnabledStatus.None)
            {
                expression = expression.And(i => i.Status == request.Status);
            }
            if (request.CourseId > 0)
            {
                expression = expression.And(i => i.CourseId == request.CourseId);
            }
            var queryable = SessionContext.Repository.QueryableCourseItem;
            if (request.CourseId > 0)
            {
                queryable = (from t1 in SessionContext.Repository.QueryableCourseItem
                             orderby t1.Id descending
                             select t1).AsPowerful();
            }
            var data = queryable.Where(expression)
               .OrderByDescending(i => i.Sort)
               //.OrderByDescending(i => i)
               .ToPagedList(request.PageIndex, request.PageSize);


            foreach (MuKeCourseCapter capter in data)
            {
                if (capter.TimeLength > 0)
                {
                    capter.TimeLengthDisplay = DateTimeUtil.GetDateStrBySecond(capter.TimeLength);
                }
                
            }
            //多种类型的内容列表，暂用同一个模板渲染
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Items", data);
            }
            ViewBag.CourseId = request.CourseId;
            return View(data);
        }

        public override PartialViewResult FormForAdd(string viewName, MuKeCourseCapter entity)
        {
            if (string.IsNullOrWhiteSpace(viewName))
            {
                viewName = "_Form";
            }
            ViewBag.OperateType = OperateTypes.Add;
            // 课程信息保留
            ViewBag.CourseID = Request.QueryString["CourseId"];
            return PartialView(viewName, entity);
        }

        public override PartialViewResult FormForUpdate(string viewName, long id)
        {
            if (string.IsNullOrWhiteSpace(viewName))
            {
                viewName = "_Form";
            }
            var entity = SessionContext.Repository.Set<MuKeCourseCapter>().Find(id);
            ViewBag.OperateType = OperateTypes.Update;
            // 课程信息保留
            ViewBag.CourseID = Request.QueryString["CourseId"];
            return PartialView(viewName, entity);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public override JsonResult Add(MuKeCourseCapter requet)
        {
            if (!string.IsNullOrWhiteSpace(requet.PlayUrl))
            {
                var videoMaterial =
                    SessionContext.Repository.MuKeMaterials.FirstOrDefault(x => x.LinkUrl == requet.PlayUrl);
                if (videoMaterial != null)
                {
                    requet.DownLoadUrl = videoMaterial.OriginalVideoUrl;
                    requet.TimeLength = videoMaterial.videoduration;
                }
            }
            JsonResult result = base.Add(requet);
            
            return result;

        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public override JsonResult Update([ModelBinder(typeof(UpdateableModelBinder))]MuKeCourseCapter requet)
        {
            if (!string.IsNullOrWhiteSpace(requet.PlayUrl))
            {
                var videoMaterial =
                    SessionContext.Repository.MuKeMaterials.FirstOrDefault(x => x.LinkUrl == requet.PlayUrl);
                if (videoMaterial != null)
                {
                    requet.DownLoadUrl = videoMaterial.OriginalVideoUrl;
                    requet.TimeLength = videoMaterial.videoduration;
                }
            }
            JsonResult result = base.Update(requet);
 
            return result;
        }
        [HttpPost]
        public override JsonResult Remove(long id)
        {
            ResultModel result;
            if (id <= 0)
            {
                result = new ResultModel(false) { Code = -1, Message = ErrorMessage.ArgumentError };
            }
            var entity = SessionContext.Repository.MuKeCourseCapters.Find(id);
            if (entity == null)
            {
                result = new ResultModel(false) { Code = -1, Message = ErrorMessage.ArgumentError };
            }
            //else //if (SessionContext.Repository.CourseItem.Any(i => i.CourseId == id))
            //{
            //    result = new ResultModel(false) { Code = -1, Message = "该视频不能删除" };
            //}
            else
            {
                SessionContext.Repository.MuKeCourseCapters.Remove(entity);
                result = SaveChanges();
            }
            return Json(result);
        }
    }
}
