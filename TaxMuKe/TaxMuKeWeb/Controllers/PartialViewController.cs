using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicUPMS.Controllers
{
    public class PartialViewController : BaseController
    {
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public PartialViewResult UploadPicture()
        {
            return PartialView();
        }


        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public PartialViewResult UploadFile()
        {
            return PartialView();
        }
    }
}