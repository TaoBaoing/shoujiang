using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BasicData.Domain;
using BasicFramework.Component;
using BasicUPMS.Models;
using BasicUPMS.Models.ViewModels.Requester;

namespace BasicUPMS.Controllers
{
    public class MuKeDirtyWordController: CURDTemplate<MuKeDirtyWord>
    {
        public ActionResult Index(MuKeDirdyWordRequest request)
        {
            IQueryable<MuKeDirtyWord> data = SessionContext.Repository.MuKeDirtyWords;
            if (!string.IsNullOrEmpty(request.Name))
            {
                data = data.Where(x => x.Name.Contains(request.Name));
            }

            var list=data.OrderByDescending(x=>x.Id).ToPagedList(request.PageIndex, request.PageSize);

            ViewBag.PageIndex = request.PageIndex;
            ViewBag.PageSize = request.PageSize;
            if (Request.IsAjaxRequest())
                return PartialView("_Items", list);
            return View(list);

        }

        public ActionResult UploadTxt(MuKeDirdyWordRequest request)
        {
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase postFile = Request.Files[file]; //get post file 
                if (postFile == null)
                {
                    continue;
                }
                if (postFile.ContentLength == 0)
                    continue;

                var fileExt = Path.GetExtension(postFile.FileName).ToLower();
                
                if (fileExt != ".txt")
                {
                    //返回
                    return Content("文件只能是.txt ");
                }
                
                using (var sr=new StreamReader(postFile.InputStream, System.Text.Encoding.Default))
                {
                    var alltext = sr.ReadToEnd();
                    if (alltext.Contains("|"))
                    {
                        var allarr = alltext.Replace(" ", "").Split('|');
                        foreach (string val in allarr)
                        {
                            var dw = new MuKeDirtyWord();
                            dw.Name = val;
                            SessionContext.Repository.MuKeDirtyWords.Add(dw);
                        }
                    }
                    else if (alltext.Contains(","))
                    {
                        var allarr = alltext.Replace(" ", "").Split(',');
                        foreach (string val in allarr)
                        {
                            var dw = new MuKeDirtyWord();
                            dw.Name = val;
                            SessionContext.Repository.MuKeDirtyWords.Add(dw);
                        }
                    }
                    else
                    {
                        while (!sr.EndOfStream)
                        {
                            var name = sr.ReadLine();
                            var dw = new MuKeDirtyWord();
                            dw.Name = name;
                            SessionContext.Repository.MuKeDirtyWords.Add(dw);
                        }
                    }

                }
                SessionContext.Repository.SaveChanges();
            }

            return RedirectToAction("Index", request);
        }
    }
}