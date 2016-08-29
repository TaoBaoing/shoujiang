using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasicData.Domain;
using BasicUPMS.Models;

namespace BasicUPMS.Controllers
{
    public class MuKeGoldUpgradeWhiteGlodSetController:BaseController
    {
        public ActionResult Index()
        {
            var set =
                SessionContext.Repository.MuKeGlobalSets.FirstOrDefault(x => x.GlobalSetType == GlobalSetType.GlodUpgradeWhiteGoldUser);
            if (set == null)
            {
                set = new MuKeGlobalSet();

            }
            return View(set);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Save(decimal money, string content,string title)
        {
            if (money == 0)
            {
                return Content("金额不能为0");
            }
            var set =
                SessionContext.Repository.MuKeGlobalSets.FirstOrDefault(x => x.GlobalSetType == GlobalSetType.GlodUpgradeWhiteGoldUser);
            if (set == null)
            {
                set = new MuKeGlobalSet();
                set.GlobalSetType = GlobalSetType.GlodUpgradeWhiteGoldUser;
                set.Content = content;
                set.Title = title;
                SessionContext.Repository.MuKeGlobalSets.Add(set);
            }
            else
            {
                set.Title = title;
                set.Content = content;
            }

            SessionContext.Repository.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}