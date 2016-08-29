using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicData;
using BasicData.Domain.Course;
using BasicFramework.Component;
using BasicUPMS.Models;
using BasicUPMS.Models.ViewModels;

namespace BasicUPMS.Controllers
{
    public class MuKeCourseItemReportController : BaseController
    {
        public ActionResult Index(MuKeRecordCourseReportRequest request)
        {
            var rccs =
                SessionContext.Repository.MuKeRecordCourseCapters.Include(x => x.Course)
                    .Include(x => x.User)
                    .Include(x => x.CourseCapter)
                    .OrderByDescending(x => x.CourseId).OrderByDescending(x=>x.CourseCapterId);

            var taxs = (from t2 in SessionContext.Repository.TaxonomyRelationships
                from t3 in SessionContext.Repository.Taxonomys
                where t2.TaxonomyId == t3.Id && t3.TaxonomyType == TaxonomyTypes.Course
                select new
                {
                    t2.ObjectId,
                    t2.TaxonomyId,
                    t3.Name,
                    t3.ParentId
                }).Distinct().ToList();

            var taxonomys = SessionContext.Repository.Taxonomys;

            var data = rccs.ToPagedList(request.PageIndex, request.PageSize);


            var list = new List<MuKeRecordCourseItemReport>();
            foreach (MuKeRecordCourseCapter cr in data)
            {
                var report = new MuKeRecordCourseItemReport();
                report.CourseId = cr.CourseId;
                report.CourseName = cr.Course.Title;
                report.CourseCapterId = cr.CourseCapterId;
                report.CourseCapterName = cr.CourseCapter.Title;
                report.UserName = cr.User.LoginName;
                if (cr.LastModifyTime.HasValue)
                {
                    report.LastModifyTiem = cr.LastModifyTime.Value.ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    report.LastModifyTiem = cr.CreateTime.ToString("yyyy-MM-dd HH:mm");
                }
                if (cr.User.UserType == MuKeUserTypes.GoldUser)
                {
                    report.UserType = "E金用户";
                }
                else if (cr.User.UserType == MuKeUserTypes.WhiteGoldUser)
                {
                    report.UserType = "E尊用户";
                }
                else
                {
                    report.UserType = "注册用户";
                }
                var deftax = taxs.FirstOrDefault(x => x.ObjectId == cr.CourseId);

                if (deftax != null)
                {
                    var tomone = taxonomys.FirstOrDefault(x => x.ParentId == deftax.ParentId);
                    if (tomone != null)
                    {
                        report.ClassOneName = tomone.Name;
                        report.ClassTwoName = deftax.Name;
                    }
                    else
                    {
                        report.ClassOneName = deftax.Name;
                    }
                }
                if (cr.CourseCapter.TimeLength > 0)
                {
                    var pp = (1.0)*cr.CurrentProcess/cr.CourseCapter.TimeLength*100;
                    var p = Math.Round(Convert.ToDecimal(pp), 2);
                    report.ViewPercent = p + "%";
                }
                else
                {
                    report.ViewPercent =  "0%";
                }
                list.Add(report);
            }

            var datalist = list.ToPagedList(0, request.PageSize, rccs.ToList().Count);

            if (Request.IsAjaxRequest())
                return PartialView("_Items", datalist);
            return View(datalist);
        }

        public ActionResult CreateExcel()
        {
            var rccs =
                SessionContext.Repository.MuKeRecordCourseCapters.Include(x => x.Course)
                    .Include(x => x.User)
                    .Include(x => x.CourseCapter)
                    .OrderByDescending(x => x.CourseId).ThenByDescending(x => x.CourseCapterId);

            var taxs = (from t2 in SessionContext.Repository.TaxonomyRelationships
                        from t3 in SessionContext.Repository.Taxonomys
                        where t2.TaxonomyId == t3.Id && t3.TaxonomyType == TaxonomyTypes.Course
                        select new
                        {
                            t2.ObjectId,
                            t2.TaxonomyId,
                            t3.Name,
                            t3.ParentId
                        }).Distinct().ToList();

            var taxonomys = SessionContext.Repository.Taxonomys.ToList();

            var data = rccs;


            var list = new List<MuKeRecordCourseItemReport>();
            foreach (MuKeRecordCourseCapter cr in data)
            {
                var report = new MuKeRecordCourseItemReport();
                report.CourseId = cr.CourseId;
                report.CourseName = cr.Course.Title;
                report.CourseCapterId = cr.CourseCapterId;
                report.CourseCapterName = cr.CourseCapter.Title;
                report.UserName = cr.User.LoginName;
                if (cr.LastModifyTime.HasValue)
                {
                    report.LastModifyTiem = cr.LastModifyTime.Value.ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    report.LastModifyTiem = cr.CreateTime.ToString("yyyy-MM-dd HH:mm");
                }
                if (cr.User.UserType == MuKeUserTypes.GoldUser)
                {
                    report.UserType = "E金用户";
                }
                else if (cr.User.UserType == MuKeUserTypes.WhiteGoldUser)
                {
                    report.UserType = "E尊用户";
                }
                else
                {
                    report.UserType = "注册用户";
                }
                var deftax = taxs.FirstOrDefault(x => x.ObjectId == cr.CourseId);

                if (deftax != null)
                {
                    var tomone = taxonomys.FirstOrDefault(x => x.ParentId == deftax.ParentId);
                    if (tomone != null)
                    {
                        report.ClassOneName = tomone.Name;
                        report.ClassTwoName = deftax.Name;
                    }
                    else
                    {
                        report.ClassOneName = deftax.Name;
                    }
                }
                if (cr.CourseCapter.TimeLength > 0)
                {
                    var pp = (1.0) * cr.CurrentProcess / cr.CourseCapter.TimeLength * 100;
                    var p = Math.Round(Convert.ToDecimal(pp), 2);
                    report.ViewPercent = p + "%";
                }
                else
                {
                    report.ViewPercent = "0%";
                }
                list.Add(report);
            }

            var datalist = list;

            var ds = new DataSet();
            using (DataTable dt = new DataTable())
            {
                //创建列
                DataColumn dtc = new DataColumn("用户", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("用户类型", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("一级分类", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("二级分类", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("课程ID", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("课程名称", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("章节ID", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("章节名称", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("观看比例", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("最后观看时间", typeof(string));
                dt.Columns.Add(dtc);

                foreach (var report in datalist)
                {
                    DataRow dr = dt.NewRow();
                    dr["用户"] = report.UserName;
                    dr["用户类型"] = report.UserType;
                    dr["一级分类"] = report.ClassOneName;
                    dr["二级分类"] = report.ClassTwoName;
                    dr["课程ID"] = report.CourseId;
                    dr["课程名称"] = report.CourseName;
                    dr["章节ID"] = report.CourseCapterId;
                    dr["章节名称"] = report.CourseCapterName;
                    dr["观看比例"] = report.ViewPercent;
                    dr["最后观看时间"] = report.LastModifyTiem;

                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }

            return ExcelFile(ds, "课程章节浏览报表");

        }
    }
}