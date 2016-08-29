using BasicUPMS.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using BasicFramework.Component;
using BasicUPMS.Resources;
using BasicData;
using BasicData.Domain.Course;
using ThirdApi.qiniu.api;

namespace BasicUPMS.Controllers
{
    public class CourseController : CURDTemplate<MuKeCourse>
    {

        public ActionResult Index(SearchContentsRequester request)
        {
            ViewBag.SearchAction = "/Course/Index";
            ViewBag.SecessIndex = "Index";
            var expression = Fast.True<MuKeCourse>();
            expression = expression.And(x => x.IsDelete == false);
            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                expression = expression.And(i => i.Title.Contains(request.Title));
            }
            if (request.Status != MuKeEnabledStatus.None)
            {
                expression = expression.And(i => i.Status == request.Status);
            }
            if (request.RecommendStatus != MuKeEnabledStatus.None)
            {
                expression = expression.And(i => i.RecommendStatus == request.RecommendStatus);
            }
            if (request.HotStatus != MuKeEnabledStatus.None)
            {
                expression = expression.And(i => i.HotStatus == request.HotStatus);
            }
            IQueryable<MuKeCourse> queryable = SessionContext.Repository.MuKeCourses;
            if (request.TaxonomyId == 0)
            {
                queryable = (from t1 in SessionContext.Repository.QueryableCourse
                                 //                             join txt in (
                                 //                             from t2 in SessionContext.Repository.TaxonomyRelationships
                                 //                             from t3 in SessionContext.Repository.Taxonomys
                                 //                             where t2.TaxonomyId == t3.Id && t3.TaxonomyType == BasicData.TaxonomyTypes.Course
                                 //                             select new {
                                 //                                 t2.ObjectId
                                 //                             }
                                 //                             )on t1.Id equals txt.ObjectId 
                             orderby t1.Id descending
                             select t1).AsPowerful();

                //from course  in SessionContext.Repository.QueryableCourse


                //queryable = from t1 in SessionContext.Repository.Course
                //            join type in SessionContext.Repository.TaxonomyRelationships
                //            on t1.Id equals type.ObjectId
                //            join tax in SessionContext.Repository.Taxonomys.Where(item => item.TaxonomyType == TaxonomyTypes.UniversityActivities)
                //            on type.TaxonomyId equals tax.Id
                //            select t1;
            }
            else
            {
                //  queryable=SessionContext.Repository.Course.SqlQuery("select * from course where id in (select ObjectId from taxonomy_relationships where TaxonomyId in(select id from taxonomy  where TaxonomyType = "+BasicData.TaxonomyTypes.Course+"))");
                queryable = (from t1 in SessionContext.Repository.QueryableCourse
                             join txt in (
                             from t2 in SessionContext.Repository.TaxonomyRelationships
                             from t3 in SessionContext.Repository.Taxonomys
                             where t2.TaxonomyId == t3.Id && t3.TaxonomyType == BasicData.TaxonomyTypes.Course && t2.TaxonomyId == request.TaxonomyId
                             select new
                             {
                                 t2.ObjectId
                             }
                             ) on t1.Id equals txt.ObjectId
                             orderby t1.Id descending
                             select t1).AsPowerful();

                //(from t1 in SessionContext.Repository.QueryableCourse
                //             from t2 in SessionContext.Repository.TaxonomyRelationships
                //             from t3 in SessionContext.Repository.Taxonomys
                //             where t1.Id == t2.ObjectId && t2.TaxonomyId == t3.Id && t3.TaxonomyType == BasicData.TaxonomyTypes.Course && t2.TaxonomyId==request.TaxonomyId
                //             orderby t1.Id descending
                //             select t1).AsPowerful();
            }
            var data = queryable.Distinct().Where(expression)
               .OrderByDescending(i => i.Sort)
               .OrderByDescending(i => i.CreateTime)
               .ToPagedList(request.PageIndex, request.PageSize);
            //多种类型的内容列表，暂用同一个模板渲染
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Items", data);
            }
            //ViewBag.TaxonomyId = request.TaxonomyId;

            //类型
            var taxonomyType = from tax in SessionContext.Repository.Taxonomys
                               where tax.Status == MuKeEnabledStatus.Enabled && tax.TaxonomyType == TaxonomyTypes.Course
                               select new
                               {
                                   id = tax.Id,
                                   pid = tax.ParentId,
                                   text = tax.Name
                               };

            ViewBag.taxonomyTypes = taxonomyType.ToList();

            return View(data);
        }
        public override PartialViewResult FormForAdd(string viewName, MuKeCourse entity)
        {

            //类型
            var taxonomyType = from tax in SessionContext.Repository.Taxonomys
                               where tax.Status == MuKeEnabledStatus.Enabled && tax.TaxonomyType == TaxonomyTypes.Course
                               select new
                               {
                                   id = tax.Id,
                                   pid = tax.ParentId,
                                   text = tax.Name
                               };
            ViewBag.Taxid = 0;
            ViewBag.taxonomyTypes = taxonomyType.ToList();
            ViewBag.SubmitCallback = "window.parent.frames['workspace'].location.href = '/Course/Index';";
            ViewBag.CourseType = CourseType.FreeUser;
            return base.FormForAdd(viewName, entity);

        }


        public override PartialViewResult FormForUpdateWithIndex(string viewName, long id, string sucessIndex)
        {

            //
            var result = base.FormForUpdate(viewName, id);
            //类型
            var taxonomyType = from tax in SessionContext.Repository.Taxonomys
                               where tax.Status == MuKeEnabledStatus.Enabled && tax.TaxonomyType == TaxonomyTypes.Course
                               select new
                               {
                                   id = tax.Id,
                                   pid = tax.ParentId,
                                   text = tax.Name
                               };

            ViewBag.taxonomyTypes = taxonomyType.ToList();
            var Taxid = SessionContext.Repository.TaxonomyRelationships.Include(x => x.Taxonomy).FirstOrDefault(i => i.ObjectId == id && i.Taxonomy.TaxonomyType == TaxonomyTypes.Course);
            if (Taxid != null)
            {
                ViewBag.Taxid = Taxid.TaxonomyId;
            }
            else
            {
                ViewBag.Taxid = 0;
            }

            ViewBag.SubmitCallback = "window.parent.frames['workspace'].location.href = '/Course/" + sucessIndex + "';";
            var course = SessionContext.Repository.MuKeCourses.Find(id);
            ViewBag.CourseType = course.CourseType;
            ViewBag.ImageUrl = QiNiuApi.GetQiNiuUrlByKey(course.ImageUrl);
            ViewBag.SearchAction = "/Course/" + sucessIndex;
            return result;

        }

        public override PartialViewResult FormForUpdate(string viewName, long id)
        {
            //
            var result = base.FormForUpdate(viewName, id);
            //类型
            var taxonomyType = from tax in SessionContext.Repository.Taxonomys
                               where tax.Status == MuKeEnabledStatus.Enabled && tax.TaxonomyType == TaxonomyTypes.Course
                               select new
                               {
                                   id = tax.Id,
                                   pid = tax.ParentId,
                                   text = tax.Name
                               };

            ViewBag.taxonomyTypes = taxonomyType.ToList();
            var Taxid = SessionContext.Repository.TaxonomyRelationships.Include(x => x.Taxonomy).FirstOrDefault(i => i.ObjectId == id && i.Taxonomy.TaxonomyType == TaxonomyTypes.Course);
            if (Taxid != null)
            {
                ViewBag.Taxid = Taxid.TaxonomyId;
            }
            else
            {
                ViewBag.Taxid = 0;
            }
            var course = SessionContext.Repository.MuKeCourses.Find(id);
            ViewBag.CourseType = course.CourseType;
            return result;
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public override JsonResult Add(MuKeCourse requet)
        {
            var taxonomyId = Request.Params["TaxonomyId"];
            if (string.IsNullOrEmpty(taxonomyId))
            {
                return Json(new ResultModel<long>(false, 0, "分类必须选择", data: (long)0));
            }

            JsonResult result = base.Add(requet);

            var intTaxonomyId = Convert.ToInt64(taxonomyId);
            SessionContext.Repository.TaxonomyRelationships.Add(new TaxonomyRelationships
            {
                ObjectId = requet.Id,
                TaxonomyId = intTaxonomyId
            });

            SaveChanges();

            return result;

        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public override JsonResult Update([ModelBinder(typeof(UpdateableModelBinder))]MuKeCourse requet)
        {


            var taxonomyId = Request.Params["TaxonomyId"];
            if (string.IsNullOrEmpty(taxonomyId))
            {
                return Json(new ResultModel<long>(false, 0, "分类必须选择", data: (long)0));
            }

            SessionContext.Repository.Entry(requet).State = EntityState.Modified;

            JsonResult result = base.Update(requet);

            var intTaxonomyId = Convert.ToInt64(taxonomyId);

            var taxmodel = SessionContext.Repository.TaxonomyRelationships.Include(x => x.Taxonomy).FirstOrDefault(i => i.ObjectId == requet.Id && i.Taxonomy.TaxonomyType == TaxonomyTypes.Course);
            if (taxmodel == null)
            {
                SessionContext.Repository.TaxonomyRelationships.Add(new TaxonomyRelationships
                {
                    ObjectId = requet.Id,
                    TaxonomyId = intTaxonomyId
                });
            }
            else
            {
                SessionContext.Repository.Entry(taxmodel).State = EntityState.Modified;
                taxmodel.TaxonomyId = intTaxonomyId;
            }
            SaveChanges();

            //            var course = SessionContext.Repository.MuKeCourses.Find(requet.Id);
            //            SessionContext.Repository.MuKeCourses.Attach(course);
            //            course.IsFree = requet.IsFree;
            //            course.IsGlod = requet.IsGlod;
            //            course.IsWhiteGlod = requet.IsWhiteGlod;
            //            course.IsNotSet = requet.IsNotSet;
            //
            //            SessionContext.Repository.Entry(course).State = EntityState.Modified;
            //            SessionContext.Repository.SaveChanges();
            return result;
        }
        [HttpPost]
        public override JsonResult Remove(long id)
        {
            //软删除
            ResultModel result;
            if (id <= 0)
            {
                result = new ResultModel(false) { Code = -1, Message = ErrorMessage.ArgumentError };
            }
            var entity = SessionContext.Repository.MuKeCourses.Find(id);
            if (entity == null)
            {
                result = new ResultModel(false) { Code = -1, Message = ErrorMessage.ArgumentError };
            }
            else
            {
                SessionContext.Repository.MuKeCourses.Attach(entity);
                entity.IsDelete = true;

                result = SaveChanges();
            }
            return Json(result);
        }


        public ActionResult CourseComments(long courseid)
        {
            var coms = SessionContext.Repository.MuKeCourseCapterComments.Where(x => x.CourseId == courseid).OrderByDescending(x => x.CreateTime);

            return View(coms);

        }

        //此处应该用一个方法
        public ActionResult NotSetIndex(SearchContentsRequester request)
        {
            ViewBag.SecessIndex = "NotSetIndex";
            var expression = Fast.True<MuKeCourse>();
            expression = expression.And(x => x.IsDelete == false);
            expression.And(x => x.CourseType == CourseType.Unknown);
            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                expression = expression.And(i => i.Title.Contains(request.Title));
            }
            if (request.Status != MuKeEnabledStatus.None)
            {
                expression = expression.And(i => i.Status == request.Status);
            }
            if (request.RecommendStatus != MuKeEnabledStatus.None)
            {
                expression = expression.And(i => i.RecommendStatus == request.RecommendStatus);
            }
            if (request.HotStatus != MuKeEnabledStatus.None)
            {
                expression = expression.And(i => i.HotStatus == request.HotStatus);
            }
            IQueryable<MuKeCourse> queryable = SessionContext.Repository.MuKeCourses;
            if (request.TaxonomyId == 0)
            {
                queryable = (from t1 in SessionContext.Repository.QueryableCourse
                             join txt in (
                             from t2 in SessionContext.Repository.TaxonomyRelationships
                             from t3 in SessionContext.Repository.Taxonomys
                             where t2.TaxonomyId == t3.Id && t3.TaxonomyType == BasicData.TaxonomyTypes.Course
                             select new
                             {
                                 t2.ObjectId
                             }
                             ) on t1.Id equals txt.ObjectId
                             orderby t1.Id descending
                             select t1).AsPowerful();

            }
            else
            {

                queryable = (from t1 in SessionContext.Repository.QueryableCourse
                             join txt in (
                             from t2 in SessionContext.Repository.TaxonomyRelationships
                             from t3 in SessionContext.Repository.Taxonomys
                             where t2.TaxonomyId == t3.Id && t3.TaxonomyType == BasicData.TaxonomyTypes.Course && t2.TaxonomyId == request.TaxonomyId
                             select new
                             {
                                 t2.ObjectId
                             }
                             ) on t1.Id equals txt.ObjectId
                             orderby t1.Id descending
                             select t1).AsPowerful();

            }
            //            var data = queryable.Where(expression).Where(x => x.CourseType == CourseType.FreeUser)
            var data = queryable.Distinct().Where(expression).Where(x => x.CourseType == CourseType.Unknown)
               .OrderByDescending(i => i.Sort)
               .ThenByDescending(i => i.CreateTime)
               .ToPagedList(request.PageIndex, request.PageSize);
            //多种类型的内容列表，暂用同一个模板渲染
            if (Request.IsAjaxRequest())
            {

                return PartialView("_Items", data);
            }
            //ViewBag.TaxonomyId = request.TaxonomyId;

            //类型
            var taxonomyType = from tax in SessionContext.Repository.Taxonomys
                               where tax.Status == MuKeEnabledStatus.Enabled && tax.TaxonomyType == TaxonomyTypes.Course
                               select new
                               {
                                   id = tax.Id,
                                   pid = tax.ParentId,
                                   text = tax.Name
                               };

            ViewBag.taxonomyTypes = taxonomyType.ToList();

            ViewBag.SearchAction = "/Course/NotSetIndex";
            return View("Index", data);
        }

        //此处应该用一个方法
        public ActionResult FreeCourseIndex(SearchContentsRequester request)
        {
            ViewBag.SecessIndex = "FreeCourseIndex";
            var expression = Fast.True<MuKeCourse>();
            expression = expression.And(x => x.IsDelete == false);
            expression.And(x => x.CourseType == CourseType.FreeUser);
            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                expression = expression.And(i => i.Title.Contains(request.Title));
            }
            if (request.Status != MuKeEnabledStatus.None)
            {
                expression = expression.And(i => i.Status == request.Status);
            }
            if (request.RecommendStatus != MuKeEnabledStatus.None)
            {
                expression = expression.And(i => i.RecommendStatus == request.RecommendStatus);
            }
            if (request.HotStatus != MuKeEnabledStatus.None)
            {
                expression = expression.And(i => i.HotStatus == request.HotStatus);
            }
            IQueryable<MuKeCourse> queryable = SessionContext.Repository.MuKeCourses;
            if (request.TaxonomyId == 0)
            {
                queryable = (from t1 in SessionContext.Repository.QueryableCourse
                             join txt in (
                             from t2 in SessionContext.Repository.TaxonomyRelationships
                             from t3 in SessionContext.Repository.Taxonomys
                             where t2.TaxonomyId == t3.Id && t3.TaxonomyType == BasicData.TaxonomyTypes.Course
                             select new
                             {
                                 t2.ObjectId
                             }
                             ) on t1.Id equals txt.ObjectId
                             orderby t1.Id descending
                             select t1).AsPowerful();

            }
            else
            {

                queryable = (from t1 in SessionContext.Repository.QueryableCourse
                             join txt in (
                             from t2 in SessionContext.Repository.TaxonomyRelationships
                             from t3 in SessionContext.Repository.Taxonomys
                             where t2.TaxonomyId == t3.Id && t3.TaxonomyType == BasicData.TaxonomyTypes.Course && t2.TaxonomyId == request.TaxonomyId
                             select new
                             {
                                 t2.ObjectId
                             }
                             ) on t1.Id equals txt.ObjectId
                             orderby t1.Id descending
                             select t1).AsPowerful();

            }
            //            var data = queryable.Where(expression).Where(x => x.CourseType == CourseType.FreeUser)
            var data = queryable.Distinct().Where(expression).Where(x => x.CourseType == CourseType.FreeUser)
               .OrderByDescending(i => i.Sort)
               .ThenByDescending(i => i.CreateTime)
               .ToPagedList(request.PageIndex, request.PageSize);
            //多种类型的内容列表，暂用同一个模板渲染
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Items", data);
            }
            //ViewBag.TaxonomyId = request.TaxonomyId;

            //类型
            var taxonomyType = from tax in SessionContext.Repository.Taxonomys
                               where tax.Status == MuKeEnabledStatus.Enabled && tax.TaxonomyType == TaxonomyTypes.Course
                               select new
                               {
                                   id = tax.Id,
                                   pid = tax.ParentId,
                                   text = tax.Name
                               };

            ViewBag.taxonomyTypes = taxonomyType.ToList();

            ViewBag.SearchAction = "/Course/FreeCourseIndex";
            return View("Index", data);
        }

        public ActionResult GlobleCourseIndex(SearchContentsRequester request)
        {
            ViewBag.SecessIndex = "GlobleCourseIndex";
            var expression = Fast.True<MuKeCourse>();
            expression = expression.And(x => x.IsDelete == false);
            expression.And(x => x.IsGlod);
            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                expression = expression.And(i => i.Title.Contains(request.Title));
            }
            if (request.Status != MuKeEnabledStatus.None)
            {
                expression = expression.And(i => i.Status == request.Status);
            }
            if (request.RecommendStatus != MuKeEnabledStatus.None)
            {
                expression = expression.And(i => i.RecommendStatus == request.RecommendStatus);
            }
            if (request.HotStatus != MuKeEnabledStatus.None)
            {
                expression = expression.And(i => i.HotStatus == request.HotStatus);
            }
            IQueryable<MuKeCourse> queryable = SessionContext.Repository.MuKeCourses;
            if (request.TaxonomyId == 0)
            {
                queryable = (from t1 in SessionContext.Repository.QueryableCourse
                             join txt in (
                             from t2 in SessionContext.Repository.TaxonomyRelationships
                             from t3 in SessionContext.Repository.Taxonomys
                             where t2.TaxonomyId == t3.Id && t3.TaxonomyType == BasicData.TaxonomyTypes.Course
                             select new
                             {
                                 t2.ObjectId
                             }
                             ) on t1.Id equals txt.ObjectId
                             orderby t1.Id descending
                             select t1).AsPowerful();

            }
            else
            {

                queryable = (from t1 in SessionContext.Repository.QueryableCourse
                             join txt in (
                             from t2 in SessionContext.Repository.TaxonomyRelationships
                             from t3 in SessionContext.Repository.Taxonomys
                             where t2.TaxonomyId == t3.Id && t3.TaxonomyType == BasicData.TaxonomyTypes.Course && t2.TaxonomyId == request.TaxonomyId
                             select new
                             {
                                 t2.ObjectId
                             }
                             ) on t1.Id equals txt.ObjectId
                             orderby t1.Id descending
                             select t1).AsPowerful();

            }
            //            var data = queryable.Where(expression).Where(x=>x.CourseType== CourseType.GoldUser)
            var data = queryable.Distinct().Where(expression).Where(x => x.IsGlod)
               .OrderByDescending(i => i.Sort)
               .OrderByDescending(i => i.CreateTime)
               .ToPagedList(request.PageIndex, request.PageSize);
            //多种类型的内容列表，暂用同一个模板渲染
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Items", data);
            }
            //ViewBag.TaxonomyId = request.TaxonomyId;

            //类型
            var taxonomyType = from tax in SessionContext.Repository.Taxonomys
                               where tax.Status == MuKeEnabledStatus.Enabled && tax.TaxonomyType == TaxonomyTypes.Course
                               select new
                               {
                                   id = tax.Id,
                                   pid = tax.ParentId,
                                   text = tax.Name
                               };

            ViewBag.taxonomyTypes = taxonomyType.ToList();

            ViewBag.SearchAction = "/Course/GlobleCourseIndex";
            return View("Index", data);
        }

        public ActionResult WhiteGlobleCourseIndex(SearchContentsRequester request)
        {
            ViewBag.SecessIndex = "WhiteGlobleCourseIndex";
            var expression = Fast.True<MuKeCourse>();
            expression = expression.And(x => x.IsDelete == false);
            expression.And(x => x.IsWhiteGlod);
            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                expression = expression.And(i => i.Title.Contains(request.Title));
            }
            if (request.Status != MuKeEnabledStatus.None)
            {
                expression = expression.And(i => i.Status == request.Status);
            }
            if (request.RecommendStatus != MuKeEnabledStatus.None)
            {
                expression = expression.And(i => i.RecommendStatus == request.RecommendStatus);
            }
            if (request.HotStatus != MuKeEnabledStatus.None)
            {
                expression = expression.And(i => i.HotStatus == request.HotStatus);
            }
            IQueryable<MuKeCourse> queryable = SessionContext.Repository.MuKeCourses;
            if (request.TaxonomyId == 0)
            {
                queryable = (from t1 in SessionContext.Repository.QueryableCourse
                             join txt in (
                             from t2 in SessionContext.Repository.TaxonomyRelationships
                             from t3 in SessionContext.Repository.Taxonomys
                             where t2.TaxonomyId == t3.Id && t3.TaxonomyType == BasicData.TaxonomyTypes.Course
                             select new
                             {
                                 t2.ObjectId
                             }
                             ) on t1.Id equals txt.ObjectId
                             orderby t1.Id descending
                             select t1).AsPowerful();

            }
            else
            {

                queryable = (from t1 in SessionContext.Repository.QueryableCourse
                             join txt in (
                             from t2 in SessionContext.Repository.TaxonomyRelationships
                             from t3 in SessionContext.Repository.Taxonomys
                             where t2.TaxonomyId == t3.Id && t3.TaxonomyType == BasicData.TaxonomyTypes.Course && t2.TaxonomyId == request.TaxonomyId
                             select new
                             {
                                 t2.ObjectId
                             }
                             ) on t1.Id equals txt.ObjectId
                             orderby t1.Id descending
                             select t1).AsPowerful();


            }
            var data = queryable.Distinct().Where(expression).Where(x => x.IsWhiteGlod)
               .OrderByDescending(i => i.Sort)
               .OrderByDescending(i => i.CreateTime)
               .ToPagedList(request.PageIndex, request.PageSize);
            //多种类型的内容列表，暂用同一个模板渲染
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Items", data);
            }
            //ViewBag.TaxonomyId = request.TaxonomyId;

            //类型
            var taxonomyType = from tax in SessionContext.Repository.Taxonomys
                               where tax.Status == MuKeEnabledStatus.Enabled && tax.TaxonomyType == TaxonomyTypes.Course
                               select new
                               {
                                   id = tax.Id,
                                   pid = tax.ParentId,
                                   text = tax.Name
                               };

            ViewBag.taxonomyTypes = taxonomyType.ToList();
            ViewBag.SearchAction = "/Course/WhiteGlobleCourseIndex";
            return View("Index", data);
        }
    }
}
