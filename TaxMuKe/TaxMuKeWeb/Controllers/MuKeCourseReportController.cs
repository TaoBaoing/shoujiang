using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicCommon;
using BasicData;
using BasicData.Domain.Course;
using BasicFramework.Component;
using BasicUPMS.Models;
using BasicUPMS.Models.ViewModels;

namespace BasicUPMS.Controllers
{
    public class MuKeCourseReportController:BaseController
    {
        public ActionResult Index(MuKeRecordCourseReportRequest request)
        {
            var courseRecord= SessionContext.Repository.MuKeRecordCourses.Include(x => x.User).Include(x => x.Course).OrderByDescending(x => x.CourseId);

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


            var testRecords = SessionContext.Repository.MuKeCourseTestRecords;
            var taxonomys = SessionContext.Repository.Taxonomys;

            var data = courseRecord.ToPagedList(request.PageIndex, request.PageSize);


            var list = new List<MuKeRecordCourseReport>();
            foreach (var cr in data)
            {
                var report=new MuKeRecordCourseReport();
                report.CourseId = cr.CourseId;
                report.CourseName = cr.Course.Title;
                report.UserName = cr.User.LoginName;
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
              
                report.IsCache = cr.Course.MuKeCourseCacheType == MuKeCourseCacheType.Allow ? "是" : "否";
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

                var test = testRecords.FirstOrDefault(x => x.UserId == cr.UserId && x.MuKeCourseId == cr.CourseId);
                if (test != null)
                {
                    report.IsTest = "是";
                    report.TestScore = test.GetCorce;
                }
                else
                {
                    report.IsTest = "否";
                }
                list.Add(report);
            }

            var datalist = list.ToPagedList(0, request.PageSize, courseRecord.ToList().Count);
           
            if (Request.IsAjaxRequest())
                return PartialView("_Items", datalist);
            return View(datalist);
        }

        public ActionResult CreateExcel()
        {
            var courseRecord = SessionContext.Repository.MuKeRecordCourses.Include(x => x.User).Include(x => x.Course).OrderByDescending(x => x.CourseId);

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

            var testRecords = SessionContext.Repository.MuKeCourseTestRecords.ToList();
            var taxonomys = SessionContext.Repository.Taxonomys.ToList();

            var data = courseRecord;
            var list = new List<MuKeRecordCourseReport>();
            foreach (var cr in data)
            {
                var report = new MuKeRecordCourseReport();
                report.CourseId = cr.CourseId;
                report.CourseName = cr.Course.Title;
                report.UserName = cr.User.LoginName;
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
                report.IsCache = cr.Course.MuKeCourseCacheType == MuKeCourseCacheType.Allow ? "是" : "否";
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
                var test = testRecords.FirstOrDefault(x => x.UserId == cr.UserId && x.MuKeCourseId == cr.CourseId);
                if (test != null)
                {
                    report.IsTest = "是";
                    report.TestScore = test.GetCorce;
                }
                else
                {
                    report.IsTest = "否";
                }
                list.Add(report);
            }
            var datalist = list;

            var ds = new DataSet();
            using (DataTable dt = new DataTable())
            {
                //创建列
                DataColumn dtc = new DataColumn("课程ID", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("用户", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("用户类型", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("一级分类", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("二级分类", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("课程名称", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("是否缓存", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("是否考试", typeof(string));
                dt.Columns.Add(dtc);
                dtc = new DataColumn("考试成绩", typeof(int));
                dt.Columns.Add(dtc);

                foreach (var report  in datalist)
                {
                    DataRow dr = dt.NewRow();
                    dr["课程ID"] = report.CourseId;
                    dr["用户"] = report.UserName;
                    dr["用户类型"] = report.UserType;
                    dr["一级分类"] = report.ClassOneName;
                    dr["二级分类"] = report.ClassTwoName;
                    dr["课程名称"] = report.CourseName;
                    dr["是否缓存"] = report.IsCache;
                    dr["是否考试"] = report.IsTest;
                    dr["考试成绩"] = report.TestScore;    
                  
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
            }

            return ExcelFile(ds, "课程浏览报表");

        }
    }
}