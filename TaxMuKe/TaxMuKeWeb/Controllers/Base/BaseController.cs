using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicCommon;
using BasicFramework.Core;
using BasicFramework.Component;
using BasicUPMS.Models;
using BasicUPMS.Resources;
using GemBox.Spreadsheet;

namespace BasicUPMS.Controllers
{
    [PermissionFilter]
    [Authorize]
    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            SessionContext.Logger.Error(filterContext.Exception.ToString());
            filterContext.Result = View("Error", ErrorMessage.UnhandledError);
        }

        private static object lockobj = new object();
        protected internal virtual FileStreamResult ExcelFile(DataSet ds, string fileDownloadName)
        {
            lock (lockobj)
            {
                var excelFile = new ExcelFile();
                foreach (DataTable dataTable in ds.Tables)
                {
                    excelFile.Worksheets.Add(dataTable.TableName).InsertDataTable(dataTable, 0, 0, true);
                }

                //保存到磁盘
                var relativeAddress = "/" + UPMSConfig.FilePhysicalPath;
                var savePath = Server.MapPath(relativeAddress);
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                var files = Directory.GetFiles(savePath, "*.xlsx", SearchOption.TopDirectoryOnly);
                foreach (string file in files)
                {
                    System.IO.File.Delete(file);
                }
                Random rd=new Random();
                var ind = rd.Next(1000, 9999);
                var newFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ind+ ".xlsx";
                var filePath = savePath + newFileName;

                excelFile.SaveXlsx(filePath, new XlsxSaveOptions());
                FileStream stream = new FileStream(filePath, FileMode.Open);
                FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                fileStreamResult.FileDownloadName = fileDownloadName + ".xlsx";
                return fileStreamResult;
            }

        }
    }
}
