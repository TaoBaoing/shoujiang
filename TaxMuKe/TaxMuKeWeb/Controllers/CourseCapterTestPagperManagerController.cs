using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BasicData;
using BasicFramework.Component;
using BasicUPMS.Models;
using BasicUPMS.Models.ViewModels.Requester;
using GemBox.Spreadsheet;

namespace BasicUPMS.Controllers
{
    public class CourseCapterTestPagperManagerController : Controller
    {

        public ActionResult DownLoadMuBan()
        {
            var fileName = Server.MapPath("~/") + "Assets/DownLoadFile/上传考试题模板.xlsx";
        
            return File(fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "上传考试题模板.xlsx");
        }

        private string GetString(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            return obj.ToString();
        }
        public ActionResult UploadTestPagper(TestPagerManagerRequest request)
        {
            try
            {
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase postFile = Request.Files[file];//get post file 
                    if (postFile == null)
                    {
                        continue;
                    }
                    if (postFile.ContentLength == 0)
                        continue;


                    //string newFilePath = @"D:/";//save path 
                    //postFile.SaveAs(newFilePath + Path.GetFileName(postFile.FileName));//save file 
                    //info.AppendFormat("Upload File:{0}/r/n", postFile.FileName);//info 
                    var fileExt = Path.GetExtension(postFile.FileName).ToLower();

                    var excelFile = new ExcelFile();
                   
                    //excelFile.SaveXlsx(nul);
                    if (fileExt == ".xls")
                    {
                        excelFile.LoadXls(postFile.InputStream, XlsOptions.None);
                    }
                    else if (fileExt == ".xlsx")
                    {
                        excelFile.LoadXlsx(postFile.InputStream, XlsxOptions.None);
                    }
                    else
                    {
                        //返回
                        return Content("文件只能是.xls 或者 .xlsx");
                    }

                    var manage = new MuKeCourseCapterTestPagperManager();
                    int testid = 1;
                    var managers = SessionContext.Repository.MuKeCourseCapterTestPagperManagers.Where(x => x.CourseId == request.CourseId).ToList();
                    if (managers.Count > 0)
                    {
                        testid = managers.Max(x => x.TestId) + 1;//第几套题
                    }

                    manage.CourseId = request.CourseId;
                    manage.TestId = testid;
                    manage.Status = MuKeEnabledStatus.Enabled;

                    ExcelWorksheet sheet = excelFile.Worksheets[0];
                    if (sheet.Rows.Count > 0)
                    {
                        var row = sheet.Rows[0];
                        foreach (ExcelCell cell in row.AllocatedCells)
                        {
                            // var clolumnName = GetString(cell.Value);
                            //                        var dc = new DataColumn(GetString(cell.Value), cell.Value.GetType());
                            //                        dt.Columns.Add(dc);
                        }

                        for (int i = 1; i < sheet.Rows.Count; i++)
                        {
                            var pager = new MuKeCourseCapterTestPagper();
                            row = sheet.Rows[i];

                            pager.PagperId = Convert.ToInt32(row.AllocatedCells[0].Value);
                            var pagperType = GetString(row.AllocatedCells[1].Value);

                            if (pagperType == "单选")
                            {
                                pager.MuKeTestPagperType = MuKeTestPagperType.Single;
                            }
                            else if (pagperType == "多选")
                            {
                                pager.MuKeTestPagperType = MuKeTestPagperType.Multiple;
                            }
                            else if (pagperType == "判断")
                            {
                                pager.MuKeTestPagperType = MuKeTestPagperType.Judge;
                            }
                            else
                            {
                                return Content("类型只能是 单选,多选,判断");
                            }
                           

                            pager.Title = GetString(row.AllocatedCells[2].Value);
                            pager.AnswerA = GetString(row.AllocatedCells[3].Value);
                            pager.AnswerB = GetString(row.AllocatedCells[4].Value);
                            pager.AnswerC = GetString(row.AllocatedCells[5].Value);
                            pager.AnswerD = GetString(row.AllocatedCells[6].Value);

                            var rightAnswer = GetString(row.AllocatedCells[7].Value);
                            
                                if (rightAnswer.Contains("A") || rightAnswer.Contains("a"))
                                {
                                    pager.AnswerAIsRight = true;
                                }
                                if (rightAnswer.Contains("B") || rightAnswer.Contains("b"))
                                {
                                    pager.AnswerBIsRight = true;
                                }
                                if (rightAnswer.Contains("C") || rightAnswer.Contains("c"))
                                {
                                    pager.AnswerCIsRight = true;
                                }
                                if (rightAnswer.Contains("D") || rightAnswer.Contains("d"))
                                {
                                    pager.AnswerDIsRight = true;
                                }
                           
                            pager.Score = Convert.ToInt32(row.AllocatedCells[8].Value);

                            manage.MuKeCourseCapterTestPagpers.Add(pager);
                            //                        foreach (ExcelCell cell in row.AllocatedCells)
                            //                        {
                            //
                            //                        }
                            //                        for (int j = 0; j < row.AllocatedCells.Count; j++)
                            //                        {
                            //                           
                            //                            //dr[j] = row.AllocatedCells[j].Value;
                            //                        }

                        }
                    }
                    else
                    {
                        return Content("没有数据");
                    }

                    SessionContext.Repository.MuKeCourseCapterTestPagperManagers.Add(manage);
                    SessionContext.Repository.SaveChanges();


                }
                //ViewData["Info"] = info;
                return RedirectToAction("Index", request);
                //return View("Index", request);
                // return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
           
        }

        public ActionResult Index(TestPagerManagerRequest request)
        {
            var course =
                SessionContext.Repository.MuKeCourses.Include(x=>x.MuKeCourseCapterTestPagperManagers).FirstOrDefault(x => x.Id == request.CourseId);

            if (course == null)
            {
                return Content("不存在该课程");
            }
            var managers = course.MuKeCourseCapterTestPagperManagers;

            ViewBag.CourseId = request.CourseId;
            ViewBag.PageIndex = request.PageIndex;
            ViewBag.PageSize = request.PageSize;
            var data = managers.AsQueryable().ToPagedList(request.PageIndex, request.PageSize);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Items", data);
            }
            return View(data);
        }

        public ActionResult TestPagerManageView(long manageId)
        {
            var manage =
                SessionContext.Repository.MuKeCourseCapterTestPagperManagers.FirstOrDefault(x => x.Id == manageId);
            if (manage == null)
            {
                return Content("错误的参数");
            }
            //manage.MuKeCourseCapterTestPagpers
            return View(manage.MuKeCourseCapterTestPagpers);
        }

        public ActionResult Remove(long courseId,long manageId)
        {
            var manage=SessionContext.Repository.MuKeCourseCapterTestPagperManagers.FirstOrDefault(x => x.Id == manageId);
            if (manage != null)
            {
                SessionContext.Repository.MuKeCourseCapterTestPagperManagers.Remove(manage);
            }

            var pagers =
                SessionContext.Repository.MuKeCourseCapterTestPagpers.Where(
                    x => x.MuKeCourseCapterTestPagperManagerId == manageId);

            SessionContext.Repository.MuKeCourseCapterTestPagpers.RemoveRange(pagers);

            SessionContext.Repository.SaveChanges();
            RouteValueDictionary rd=new RouteValueDictionary();
            rd.Add("CourseId", courseId);
            rd.Add("PageIndex", 1);
            rd.Add("PageSize", 10);//考试只支持 5 套题
            return RedirectToAction("Index", rd);
        }
    }
}