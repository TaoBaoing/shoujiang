using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using ThirdApi;

namespace BasicUPMS.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 上传文件到本地
        /// </summary>
        /// <returns>返回文件名称（原来文件名称及新名称）和路径</returns>
        public ActionResult UploadPic()
        {
            try
            {
                HttpPostedFileBase file = Request.Files["file"];
                if (file != null)
                {
                    ////文件web地址
                    string webPath = ConfigurationManager.AppSettings["FilePath"] + "Images/";
                    ////文件存放路径
                    string filePath = Server.MapPath(webPath);
                    ////如果文件路径不存在。创建一个。
                    if (Directory.Exists(filePath) == false)
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    ////获取文件信息
                    FileInfo fileInfo = new FileInfo(file.FileName);
                    string oldName = fileInfo.Name;
                    ////文件扩展名
                    string fileExtend = oldName.Substring(oldName.LastIndexOf("."));
                    ////文件新名
                    string newName = "WelcomePicture" + fileExtend;
                    ////保存文件
                    file.SaveAs(filePath + newName);
                    return Json(new { result = 1 }, "text/x-json");
                }
                return Json(new { result = 0, msg = "未找到上传文件" }, "text/x-json");
            }
            catch (Exception e)
            {
                return Json(new { result = 0, msg = e.Message }, "text/x-json");
            }
        }



//        public ActionResult UploadImgsQiNiu()
//        {
//            //return Json(QiNiuApi.UploadImgs()) ;
//            return new OkResult();
//        }

    }
}