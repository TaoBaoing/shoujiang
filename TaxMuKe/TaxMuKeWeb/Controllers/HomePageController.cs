using BasicData;
using BasicFramework.Component;
using BasicUPMS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicData.Domain;

namespace BasicUPMS.Controllers
{
    public class HomePageController : CURDTemplate<VersionManage>
    {

        public ActionResult AddUpdateHomeCourse()
        {
            var data = SessionContext.Repository.MuKeHomeCourseSets.Include(x => x.HomeHotCourse1).Include(x => x.HomeHotCourse2).Include(x => x.HomeHotCourse3).Include(x => x.HomeHotCourse4).Include(x => x.HomeReCourse1).Include(x => x.HomeReCourse2).Include(x => x.HomeReCourse3).Include(x => x.HomeReCourse4).Include(x => x.PersonCourse1).Include(x => x.PersonCourse2).Include(x => x.PersonCourse3).Include(x => x.PersonCourse4).FirstOrDefault();
//            var data = SessionContext.Repository.MuKeHomeCourseSets.FirstOrDefault();
            if (data == null)
            {
                ViewBag.Id = 0;
                data = new MuKeHomeCourseSet();
            }
            else
            {
                ViewBag.Id = data.Id;
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult AddUpdateHomeCourse(MuKeHomeCourseSet requet)
        {

            var model = SessionContext.Repository.MuKeHomeCourseSets.FirstOrDefault(item => item.Id == requet.Id);

            if (model == null)
            {
                    SessionContext.Repository.Set<MuKeHomeCourseSet>().Add(requet);
            }
            else
            {
                SessionContext.Repository.Set<MuKeHomeCourseSet>().Attach(model);
                model.HomeHotCourse1Id = requet.HomeHotCourse1Id;
                model.HomeHotCourse2Id = requet.HomeHotCourse2Id;
                model.HomeHotCourse3Id = requet.HomeHotCourse3Id;
                model.HomeHotCourse4Id = requet.HomeHotCourse4Id;
                model.HomeReCourse1Id = requet.HomeReCourse1Id;
                model.HomeReCourse2Id = requet.HomeReCourse2Id;
                model.HomeReCourse3Id = requet.HomeReCourse3Id;
                model.HomeReCourse4Id = requet.HomeReCourse4Id;
                model.PersonCourse1Id = requet.PersonCourse1Id;
                model.PersonCourse2Id = requet.PersonCourse2Id;
                model.PersonCourse3Id = requet.PersonCourse3Id;
                model.PersonCourse4Id = requet.PersonCourse4Id;

            }
            SessionContext.Repository.SaveChanges();
            return AddUpdateHomeCourse();

        }

        // GET: HomePage
        public ActionResult WelcomeImage()
        {
            return View();
        }

        /// <summary>
        /// APP 
        /// </summary>
        /// <returns></returns>
        public ActionResult AppUpdate()
        {
            var data = SessionContext.Repository.VersionManage.FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult AddOrUpdate([ModelBinder(typeof(UpdateableModelBinder))]VersionManage requet)
        {
            HttpPostedFileBase fileApk = Request.Files["fileApk"];
            if (fileApk != null && fileApk.ContentLength > 0)
            {
                ////文件web地址
                string webPath = ConfigurationManager.AppSettings["FilePath"] + "APP/";
                ////文件存放路径
                string filePath = Server.MapPath(webPath);
                ////如果文件路径不存在。创建一个。
                if (Directory.Exists(filePath) == false)
                {
                    Directory.CreateDirectory(filePath);
                }
                ////获取文件信息
                FileInfo fileInfo = new FileInfo(fileApk.FileName);
                string strName = DateTime.Now.Ticks.ToString();
                ////文件扩展名
                string fileExtend = fileInfo.Name.Substring(fileInfo.Name.LastIndexOf("."));
                ////保存文件
                //                file.SaveAs(filePath + strName + fileExtend);
                //                requet.ApkUrl = webPath + strName + fileExtend;
                fileApk.SaveAs(filePath + fileApk.FileName);
                requet.ApkUrl = webPath + fileApk.FileName;
            }

            HttpPostedFileBase fileIpa = Request.Files["fileIpa"];
            if (fileIpa != null && fileIpa.ContentLength > 0)
            {
                ////文件web地址
                string webPath = ConfigurationManager.AppSettings["FilePath"] + "APP/";
                ////文件存放路径
                string filePath = Server.MapPath(webPath);
                ////如果文件路径不存在。创建一个。
                if (Directory.Exists(filePath) == false)
                {
                    Directory.CreateDirectory(filePath);
                }
                ////获取文件信息
                FileInfo fileInfo = new FileInfo(fileIpa.FileName);
                string strName = DateTime.Now.Ticks.ToString();
                ////文件扩展名
                string fileExtend = fileInfo.Name.Substring(fileInfo.Name.LastIndexOf("."));
                ////保存文件
                //                file.SaveAs(filePath + strName + fileExtend);
                //                requet.ApkUrl = webPath + strName + fileExtend;
                fileIpa.SaveAs(filePath + fileIpa.FileName);
                requet.IpaUrl = webPath + fileIpa.FileName;
            }

            // 版本名是否为空
            if (!string.IsNullOrWhiteSpace(requet.AndroidVersion) && !string.IsNullOrWhiteSpace(requet.IosVersion))
            {
                var model = SessionContext.Repository.VersionManage.FirstOrDefault(item => item.Id == requet.Id);
                if (model == null)
                {
                    // 创建时间
                    requet.CreateTime = DateTime.Now;
                    base.Add(requet);
                }
                else
                {
                    // 创建时间
                    base.Update(requet);
                }
            }
            else
            {
                return Json(new ResultModel<long> { Message = "参数无效，请核对后重新输入!", IsSuccess = false });
            }
            return RedirectToAction("AppUpdate");
        }


        /// <summary>
        /// 上传文件到本地
        /// </summary>
        /// <returns>返回文件名称（原来文件名称及新名称）和路径</returns>
        public ActionResult UploadAPP()
        {
            try
            {
                HttpPostedFileBase file = Request.Files["file"];
                if (file != null)
                {
                    ////文件web地址
                    string webPath = ConfigurationManager.AppSettings["FilePath"] + "APP/";
                    ////文件存放路径
                    string filePath = Server.MapPath(webPath);
                    ////如果文件路径不存在。创建一个。
                    if (Directory.Exists(filePath) == false)
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    ////获取文件信息
                    FileInfo fileInfo = new FileInfo(file.FileName);
                    string strName = fileInfo.Name;
                    ////文件扩展名
                    string fileExtend = strName.Substring(strName.LastIndexOf("."));
                    ////保存文件
                    file.SaveAs(filePath + strName);
                    return Json(new { result = 1, Path = webPath }, "text/x-json");
                }
                return Json(new { result = 0, msg = "未找到上传文件" }, "text/x-json");
            }
            catch (Exception e)
            {
                return Json(new { result = 0, msg = e.Message }, "text/x-json");
            }
        }
    }
}